using DimensionClient.Common;
using DimensionClient.Models.ResultModels;

namespace DimensionClient.Models.ViewModels
{
    public class ContactPersonViewModel : ModelBase
    {
        private bool slidingBlockState;
        private List<FriendSortModel> friends;
        private List<NewFriendBriefModel> newFriends;

        /// <summary>
        /// 上部"好友/新朋友"选中项
        /// </summary>
        public bool SlidingBlockState
        {
            get => slidingBlockState;
            set
            {
                slidingBlockState = value;
                OnPropertyChanged(nameof(SlidingBlockState));
            }
        }
        public List<FriendSortModel> Friends
        {
            get => friends;
            set
            {
                friends = value;
                OnPropertyChanged(nameof(Friends));
            }
        }
        public List<NewFriendBriefModel> NewFriends
        {
            get => newFriends;
            set
            {
                newFriends = value;
                OnPropertyChanged(nameof(NewFriends));
            }
        }

        public override void InitializeVariable()
        {
            slidingBlockState = true;
            friends = null;
            newFriends = null;
        }
    }
}