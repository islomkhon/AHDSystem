using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HCMApi.Modal
{
    public class UiDropdownItem
    {
        public string label { get; set; }
        public string value { get; set; }
        public UiDropdownItem()
        {

        }
        public UiDropdownItem(string label, string value)
        {
            this.label = label;
            this.value = value;
        }
    }

    public class MichaelRequestCommentRequest
    {
        public string RequestComment { get; set; }
        public string RequestId { get; set; }
        public int UserId { get; set; }

    }

    public class BotIntnetItem
    {
        public string id { get; set; }
        public string component { get; set; }
        public bool user { get; set; }
        public bool waitAction { get; set; }
        public string message { get; set; }
        public string trigger { get; set; }
        public List<Option> options { get; set; }
        public BotIntnetItem()
        {

        }
        public BotIntnetItem(string id, string message, string trigger)
        {
            this.id = id;
            this.message = message;
            this.trigger = trigger;
        }
        public BotIntnetItem(string id, List<Option> options)
        {
            this.id = id;
            this.options = options;
        }
        public BotIntnetItem(string id, string component, bool waitAction, string trigger)
        {
            this.id = id;
            this.component = component;
            this.waitAction = waitAction;
            this.trigger = trigger;
        }
        public BotIntnetItem(string id, bool user, string trigger)
        {
            this.id = id;
            this.user = user;
            this.trigger = trigger;
        }


    }

    public class Option
    {
        public string value { get; set; }
        public string label { get; set; }
        public string trigger { get; set; }
    }

    public class MichaelRequestFeedbackRequest
    {
        public int Ratting { get; set; }
        public string RequestComment { get; set; }
        public string RequestId { get; set; }
        public int UserId { get; set; }

    }

    public class MichaelRequestLogItem
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string DateOn { get; set; }
        public string Icon { get; set; }
        public string Payload { get; set; }
        public string PayloadType { get; set; }
        public int AttachmentId { get; set; }
    }

    public class MichaelRequestAttachment
    {
        public IFormFile file { get; set; }
        public string RequestId { get; set; }
        public int UserId { get; set; }

    }

    public class MichaelRequestUserOps
    {
        public int Comment { get; set; }
        public int Attach { get; set; }
        public int Close { get; set; }
        public int Withdraw { get; set; }
        public int Approve { get; set; }
        public int ApproveReject { get; set; }
        public int AdminApprove { get; set; }
        public int AdminOverideApprove { get; set; }

        public MichaelRequestUserOps()
        {
            this.Comment = 0;
            this.Attach = 0;
            this.Close = 0;
            this.ApproveReject = 0;
            this.Withdraw = 0;
            this.Approve = 0;
            this.AdminApprove = 0;
            this.AdminOverideApprove = 0;
        }
    }

    public class SideBarItem
    {
        public string key { get; set; }
        public string value { get; set; }
        public SideBarItem()
        {

        }
        public SideBarItem(string key, string value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public class MichaelRequestUserAccess
    {
        public int IsAssignee { get; set; }
        public int IsApprover { get; set; }
        public int IsOwner { get; set; }

        public MichaelRequestUserAccess()
        {
            this.IsAssignee = 0;
            this.IsApprover = 0;
            this.IsOwner = 0;
        }
    }

    public class MichaelRequestViewerData
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public string RequestType { get; set; }
        public DAL.MichaelRequestMaster michaelRequestBase { get; set; }
        public int UserId { get; set; }
        public int IsPermitted { get; set; }
        public int IsError { get; set; }
        public string ErrorMessage { get; set; }
        public int RequestStatusId { get; set; }
        public int IsApprovalProcess { get; set; }
        public int IsApprovalProcessComplated { get; set; }
        public string RequestStatus { get; set; }
        public MichaelRequestUserOps MichaelRequestUserOp { get; set; }
        public MichaelRequestUserAccess MichaelRequestUserAcces { get; set; }
        public List<SideBarItem> SidebarData { get; set; }
    }


    public class MichaelRequestBotPrevData
    {
        public string ResponseMode { get; set; }
        public int IsPermitted { get; set; }
        public MichaelRequestViewerData michaelRequestViewerData;

    }

    public class MichaeNotificationPayload
    {
        public int MessageId { get; set; }
        public string Message { get; set; }
        public string EmptyMessage { get; set; }
        public int Processed { get; set; }
        public int UserId { get; set; }
        public string Target { get; set; }
        public DateTime Date { get; set; }
        public string DateAdded { get; set; }
    }

    public class MichaeNotificationBase
    {
        public List<MichaeNotificationPayload> MichaeNotificationPayloads { get; set; }
        public int Count { get; set; }
    }

    public class MichaeNotification
    {
        public string Channel { get; set; }
        public MichaeNotificationBase michaeNotificationBase { get; set; }

    }

    public class MichaeRequestSummaryItem
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string RequestId { get; set; }
        public string RequestType { get; set; }
        public string RequestStatus { get; set; }
        public string DateAdded { get; set; }
        public string DateModified { get; set; }
    }

    public class MichaeRequestAcessItem
    {
        public int UserId { get; set; }
        public string AcessType { get; set; }
    }

    public class MichaeUserAcess
    {
        DAL.NueUserProfile UserProfile { get; set; }
        public string AcessType { get; set; }

        public MichaeUserAcess()
        {

        }

        public MichaeUserAcess(DAL.NueUserProfile UserProfile, string AcessType)
        {
            this.UserProfile = UserProfile;
            this.AcessType = AcessType;
        }
    }

    public class MichaeAdminUserRequest
    {
        public List<UiDropdownItem> AdminUserList { get; set; }
    }

    public class MichaeUserAccess
    {
        public string AcessType { get; set; }
        public int IsAssignee { get; set; }
    }

}
