﻿using DimensionClient.Common;
using DimensionClient.Models.ResultModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace DimensionClient.Service.UserManager
{
    public static class UserManagerService
    {
        public static bool UserLogin(string loginName, string password, DateTime loginTime, out UserLoginModel userLoginModel)
        {
            userLoginModel = null;
            JObject requestObj = new()
            {
                { "LoginName", loginName },
                { "Password", password },
                { "UseDevice", ClassHelper.device.ToString() },
                { "LoginTime", loginTime }
            };
            if (ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/UserLogin", HttpMethod.Post, out JObject responseObj, requestObj: requestObj))
            {
                userLoginModel = JsonConvert.DeserializeObject<UserLoginModel>(responseObj["Data"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetVerificationCode(string verifyAccount)
        {
            JObject requestObj = new()
            {
                { "VerifyAccount", verifyAccount },
                { "UseDevice", ClassHelper.device.ToString() }
            };
            return ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/GetVerificationCode", HttpMethod.Post, out _, requestObj: requestObj);
        }

        public static bool PhoneNumberLogin(string phoneNumber, string verifyCode, out UserLoginModel userLoginModel)
        {
            userLoginModel = null;
            JObject requestObj = new()
            {
                { "PhoneNumber", phoneNumber },
                { "VerifyCode", verifyCode },
                { "UseDevice", ClassHelper.device.ToString() }
            };
            if (ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/PhoneNumberLogin", HttpMethod.Post, out JObject responseObj, requestObj: requestObj))
            {
                userLoginModel = JsonConvert.DeserializeObject<UserLoginModel>(responseObj["Data"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetUserInfo(out GetUserInfoModel getUserInfoModel)
        {
            getUserInfoModel = null;
            if (ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/GetUserInfo", HttpMethod.Get, out JObject responseObj))
            {
                getUserInfoModel = JsonConvert.DeserializeObject<GetUserInfoModel>(responseObj["Data"].ToString());
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool GetFriendList(out List<FriendSortModel> friendSorts)
        {
            friendSorts = null;
            if (ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/GetFriendList", HttpMethod.Get, out JObject responseObj))
            {
                friendSorts = JsonConvert.DeserializeObject<List<FriendSortModel>>(responseObj["Data"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="friendID"></param>
        /// <param name="verifyInfo"></param>
        /// <returns></returns>
        public static bool FriendRegistration(string friendID, string verifyInfo)
        {
            JObject requestObj = new()
            {
                { "FriendID", friendID },
                { "VerifyInfo", verifyInfo }
            };
            return ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/FriendRegistration", HttpMethod.Post, out _, requestObj: requestObj);
        }

        public static bool FriendValidation(string friendID, bool passed)
        {
            JObject requestObj = new()
            {
                { "FriendID", friendID },
                { "Passed", passed }
            };
            return ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/FriendValidation", HttpMethod.Post, out _, requestObj: requestObj);
        }

        public static bool GetNewFriendList(out List<NewFriendBriefModel> newFriendBriefs)
        {
            newFriendBriefs = null;
            if (ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/GetNewFriendList", HttpMethod.Get, out JObject responseObj))
            {
                newFriendBriefs = JsonConvert.DeserializeObject<List<NewFriendBriefModel>>(responseObj["Data"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool GetFriendInfo(out FriendDetailsModel friendDetails, string friendID = "", string phoneNumber = "")
        {
            friendDetails = null;
            if (ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/GetFriendInfo?{(!string.IsNullOrEmpty(friendID) ? $"friendID={friendID}" : $"phoneNumber={phoneNumber}")}", HttpMethod.Get, out JObject responseObj))
            {
                friendDetails = JsonConvert.DeserializeObject<FriendDetailsModel>(responseObj["Data"].ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateRemarkInfo(string friendID, string remarkName = null, string remarkInformation = null)
        {
            JObject requestObj = new()
            {
                { "FriendID", friendID }
            };
            if (remarkName != null)
            {
                requestObj.Add("RemarkName", remarkName);
            }
            if (remarkInformation != null)
            {
                requestObj.Add("RemarkInformation", remarkInformation);
            }
            return ClassHelper.ServerRequest($"{ClassHelper.servicePath}/api/UserManager/UpdateRemarkInfo", HttpMethod.Post, out _, requestObj: requestObj);
        }
    }
}
