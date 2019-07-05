using System;
using System.Collections.Generic;

namespace HCMApi.DAL
{
    public partial class NueUserProfile
    {
        public NueUserProfile()
        {
            MichaelDepartmentMaster = new HashSet<MichaelDepartmentMaster>();
            MichaelDepartmentRequestTypeMaster = new HashSet<MichaelDepartmentRequestTypeMaster>();
            NeuEmployeeVerificationRequest = new HashSet<NeuEmployeeVerificationRequest>();
            NeuMessages = new HashSet<NeuMessages>();
            NeuUserPreferenceFirstApproverNavigation = new HashSet<NeuUserPreference>();
            NeuUserPreferenceSecondApproverNavigation = new HashSet<NeuUserPreference>();
            NeuUserPreferenceUser = new HashSet<NeuUserPreference>();
            NueAccessMapper = new HashSet<NueAccessMapper>();
            NueAddressProofRequest = new HashSet<NueAddressProofRequest>();
            NueDblocationChangeRequest = new HashSet<NueDblocationChangeRequest>();
            NueDbmanagerChangeRequestManager = new HashSet<NueDbmanagerChangeRequest>();
            NueDbmanagerChangeRequestUser = new HashSet<NueDbmanagerChangeRequest>();
            NueDomesticTripRequest = new HashSet<NueDomesticTripRequest>();
            NueGeneralRequest = new HashSet<NueGeneralRequest>();
            NueGoalAccessMapperOwner = new HashSet<NueGoalAccessMapper>();
            NueGoalAccessMapperUser = new HashSet<NueGoalAccessMapper>();
            NueGoalGlobelRepo = new HashSet<NueGoalGlobelRepo>();
            NueGoalLocalRepoInitiOwnerNavigation = new HashSet<NueGoalLocalRepo>();
            NueGoalLocalRepoUser = new HashSet<NueGoalLocalRepo>();
            NueGoalStatusMapperOwner = new HashSet<NueGoalStatusMapper>();
            NueGoalStatusMapperUser = new HashSet<NueGoalStatusMapper>();
            NueInternationalTripRequest = new HashSet<NueInternationalTripRequest>();
            NueLeaveBalanceEnquiryRequest = new HashSet<NueLeaveBalanceEnquiryRequest>();
            NueLeaveCancelationRequest = new HashSet<NueLeaveCancelationRequest>();
            NueLeavePastApplyRequest = new HashSet<NueLeavePastApplyRequest>();
            NueLeaveWfhapplyRequest = new HashSet<NueLeaveWfhapplyRequest>();
            NueManagerMapperManager = new HashSet<NueManagerMapper>();
            NueManagerMapperUser = new HashSet<NueManagerMapper>();
            NuePgbrequest = new HashSet<NuePgbrequest>();
            NuePgbrequestUsers = new HashSet<NuePgbrequestUsers>();
            NueRequestAceessLogOwner = new HashSet<NueRequestAceessLog>();
            NueRequestAceessLogUser = new HashSet<NueRequestAceessLog>();
            NueRequestActivity = new HashSet<NueRequestActivity>();
            NueRequestAttachmentLogOwner = new HashSet<NueRequestAttachmentLog>();
            NueRequestAttachmentLogUser = new HashSet<NueRequestAttachmentLog>();
            NueRequestMaster = new HashSet<NueRequestMaster>();
            NueSalaryCertificateRequest = new HashSet<NueSalaryCertificateRequest>();
            NueUserOrgMapperOrgUser = new HashSet<NueUserOrgMapper>();
            NueUserOrgMapperUser = new HashSet<NueUserOrgMapper>();
        }

        public int Id { get; set; }
        public string Ntplid { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? EmploymentStatus { get; set; }
        public string DateofJoining { get; set; }
        public int? Practice { get; set; }
        public string Location { get; set; }
        public int? JobLevel { get; set; }
        public int? Designation { get; set; }
        public int? Active { get; set; }
        public DateTime? AddedOn { get; set; }

        public virtual NeuDesignation DesignationNavigation { get; set; }
        public virtual NeuEmploymentStatus EmploymentStatusNavigation { get; set; }
        public virtual NeuJobLevel JobLevelNavigation { get; set; }
        public virtual NeuPractice PracticeNavigation { get; set; }
        public virtual ICollection<MichaelDepartmentMaster> MichaelDepartmentMaster { get; set; }
        public virtual ICollection<MichaelDepartmentRequestTypeMaster> MichaelDepartmentRequestTypeMaster { get; set; }
        public virtual ICollection<NeuEmployeeVerificationRequest> NeuEmployeeVerificationRequest { get; set; }
        public virtual ICollection<NeuMessages> NeuMessages { get; set; }
        public virtual ICollection<NeuUserPreference> NeuUserPreferenceFirstApproverNavigation { get; set; }
        public virtual ICollection<NeuUserPreference> NeuUserPreferenceSecondApproverNavigation { get; set; }
        public virtual ICollection<NeuUserPreference> NeuUserPreferenceUser { get; set; }
        public virtual ICollection<NueAccessMapper> NueAccessMapper { get; set; }
        public virtual ICollection<NueAddressProofRequest> NueAddressProofRequest { get; set; }
        public virtual ICollection<NueDblocationChangeRequest> NueDblocationChangeRequest { get; set; }
        public virtual ICollection<NueDbmanagerChangeRequest> NueDbmanagerChangeRequestManager { get; set; }
        public virtual ICollection<NueDbmanagerChangeRequest> NueDbmanagerChangeRequestUser { get; set; }
        public virtual ICollection<NueDomesticTripRequest> NueDomesticTripRequest { get; set; }
        public virtual ICollection<NueGeneralRequest> NueGeneralRequest { get; set; }
        public virtual ICollection<NueGoalAccessMapper> NueGoalAccessMapperOwner { get; set; }
        public virtual ICollection<NueGoalAccessMapper> NueGoalAccessMapperUser { get; set; }
        public virtual ICollection<NueGoalGlobelRepo> NueGoalGlobelRepo { get; set; }
        public virtual ICollection<NueGoalLocalRepo> NueGoalLocalRepoInitiOwnerNavigation { get; set; }
        public virtual ICollection<NueGoalLocalRepo> NueGoalLocalRepoUser { get; set; }
        public virtual ICollection<NueGoalStatusMapper> NueGoalStatusMapperOwner { get; set; }
        public virtual ICollection<NueGoalStatusMapper> NueGoalStatusMapperUser { get; set; }
        public virtual ICollection<NueInternationalTripRequest> NueInternationalTripRequest { get; set; }
        public virtual ICollection<NueLeaveBalanceEnquiryRequest> NueLeaveBalanceEnquiryRequest { get; set; }
        public virtual ICollection<NueLeaveCancelationRequest> NueLeaveCancelationRequest { get; set; }
        public virtual ICollection<NueLeavePastApplyRequest> NueLeavePastApplyRequest { get; set; }
        public virtual ICollection<NueLeaveWfhapplyRequest> NueLeaveWfhapplyRequest { get; set; }
        public virtual ICollection<NueManagerMapper> NueManagerMapperManager { get; set; }
        public virtual ICollection<NueManagerMapper> NueManagerMapperUser { get; set; }
        public virtual ICollection<NuePgbrequest> NuePgbrequest { get; set; }
        public virtual ICollection<NuePgbrequestUsers> NuePgbrequestUsers { get; set; }
        public virtual ICollection<NueRequestAceessLog> NueRequestAceessLogOwner { get; set; }
        public virtual ICollection<NueRequestAceessLog> NueRequestAceessLogUser { get; set; }
        public virtual ICollection<NueRequestActivity> NueRequestActivity { get; set; }
        public virtual ICollection<NueRequestAttachmentLog> NueRequestAttachmentLogOwner { get; set; }
        public virtual ICollection<NueRequestAttachmentLog> NueRequestAttachmentLogUser { get; set; }
        public virtual ICollection<NueRequestMaster> NueRequestMaster { get; set; }
        public virtual ICollection<NueSalaryCertificateRequest> NueSalaryCertificateRequest { get; set; }
        public virtual ICollection<NueUserOrgMapper> NueUserOrgMapperOrgUser { get; set; }
        public virtual ICollection<NueUserOrgMapper> NueUserOrgMapperUser { get; set; }
    }
}
