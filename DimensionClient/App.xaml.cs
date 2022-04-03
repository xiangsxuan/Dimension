﻿using DimensionClient.Common;
using DimensionClient.Component.Windows;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace DimensionClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // 异常崩溃处理
            if (ThreadPool.SetMaxThreads(25, 25))
            {
                DispatcherUnhandledException += App_DispatcherUnhandledException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            };

            // 汪凯说过? 这个有啥用来着?
            if (SystemParameters.MenuDropAlignment)
            {
                FieldInfo field = typeof(SystemParameters).GetField("_menuDropAlignment", BindingFlags.NonPublic | BindingFlags.Static);
                field.SetValue(null, false);
            }

            // 设置字体
            FontFamily fontFamily = new(new Uri("pack://application:,,,/"), "/Library/Font/苹方黑体-中粗-简.ttf#.萍方-简");
            Control.FontFamilyProperty.OverrideMetadata(typeof(TextBoxBase), new FrameworkPropertyMetadata(fontFamily));
            Control.FontFamilyProperty.OverrideMetadata(typeof(ContentControl), new FrameworkPropertyMetadata(fontFamily));
            TextElement.FontFamilyProperty.OverrideMetadata(typeof(TextElement), new FrameworkPropertyMetadata(fontFamily));
            TextBlock.FontFamilyProperty.OverrideMetadata(typeof(TextBlock), new FrameworkPropertyMetadata(fontFamily));
            FrameworkElement.FocusVisualStyleProperty.OverrideMetadata(typeof(Control), new FrameworkPropertyMetadata(defaultValue: null));

            // 在设置窗体的样式?
            BitmapCache bitmapCache = new()
            {
                RenderAtScale = 2,
                SnapsToDevicePixels = true,
                EnableClearType = true
            };
            UIElement.CacheModeProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(bitmapCache));
            UIElement.CacheModeProperty.OverrideMetadata(typeof(MainWindow), new FrameworkPropertyMetadata(defaultValue: null));
            UIElement.CacheModeProperty.OverrideMetadata(typeof(Screenshots), new FrameworkPropertyMetadata(defaultValue: null));

            // UI的进程调度保存全局
            ClassHelper.Dispatcher = Dispatcher;

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            if (ClassHelper.CallViewManager != null)
            {
                ClassHelper.CallViewManager.UnInitialize();

                // 由于卸载为异步操作，强制主线程睡一秒，释放资源。
                Thread.Sleep(1000);
            }

            Environment.Exit(-1);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // 为什么这两数值?
            if (e.Exception.HResult is not (-2147221037) and not (-2146233079))
            {
                ClassHelper.RecordException(typeof(App), e.Exception);
            }
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ClassHelper.RecordException(typeof(App), e.ExceptionObject as Exception);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            ClassHelper.RecordException(typeof(App), e.Exception);
            e.SetObserved();
        }
    }
}
