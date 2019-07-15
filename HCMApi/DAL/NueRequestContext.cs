﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HCMApi.DAL
{
    public partial class NueRequestContext : DbContext
    {
        public NueRequestContext()
        {
        }

        public NueRequestContext(DbContextOptions<NueRequestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
        public virtual DbSet<Hash> Hash { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobParameter> JobParameter { get; set; }
        public virtual DbSet<JobQueue> JobQueue { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<MichaelApproverStatusTypes> MichaelApproverStatusTypes { get; set; }
        public virtual DbSet<MichaelDepartmentMaster> MichaelDepartmentMaster { get; set; }
        public virtual DbSet<MichaelDepartmentRequestMaster> MichaelDepartmentRequestMaster { get; set; }
        public virtual DbSet<MichaelEscalationBase> MichaelEscalationBase { get; set; }
        public virtual DbSet<MichaelPayloadDataType> MichaelPayloadDataType { get; set; }
        public virtual DbSet<MichaelRequestAccessMapper> MichaelRequestAccessMapper { get; set; }
        public virtual DbSet<MichaelRequestAccessTypes> MichaelRequestAccessTypes { get; set; }
        public virtual DbSet<MichaelRequestApproverStatusMapper> MichaelRequestApproverStatusMapper { get; set; }
        public virtual DbSet<MichaelRequestAttachmentLog> MichaelRequestAttachmentLog { get; set; }
        public virtual DbSet<MichaelRequestEscalationAccessLogs> MichaelRequestEscalationAccessLogs { get; set; }
        public virtual DbSet<MichaelRequestEscalationDurationLogs> MichaelRequestEscalationDurationLogs { get; set; }
        public virtual DbSet<MichaelRequestEscalationMapper> MichaelRequestEscalationMapper { get; set; }
        public virtual DbSet<MichaelRequestEscalationUserBaseMapper> MichaelRequestEscalationUserBaseMapper { get; set; }
        public virtual DbSet<MichaelRequestLog> MichaelRequestLog { get; set; }
        public virtual DbSet<MichaelRequestLogTypes> MichaelRequestLogTypes { get; set; }
        public virtual DbSet<MichaelRequestMaster> MichaelRequestMaster { get; set; }
        public virtual DbSet<MichaelRequestPayloadMaster> MichaelRequestPayloadMaster { get; set; }
        public virtual DbSet<MichaelRequestPriority> MichaelRequestPriority { get; set; }
        public virtual DbSet<MichaelRequestStageBase> MichaelRequestStageBase { get; set; }
        public virtual DbSet<MichaelRequestStageLogs> MichaelRequestStageLogs { get; set; }
        public virtual DbSet<NeuCountry> NeuCountry { get; set; }
        public virtual DbSet<NeuDesignation> NeuDesignation { get; set; }
        public virtual DbSet<NeuEmployeeVerificationRequest> NeuEmployeeVerificationRequest { get; set; }
        public virtual DbSet<NeuEmploymentStatus> NeuEmploymentStatus { get; set; }
        public virtual DbSet<NeuJobLevel> NeuJobLevel { get; set; }
        public virtual DbSet<NeuMessages> NeuMessages { get; set; }
        public virtual DbSet<NeuPractice> NeuPractice { get; set; }
        public virtual DbSet<NeuUserDetail> NeuUserDetail { get; set; }
        public virtual DbSet<NeuUserPreference> NeuUserPreference { get; set; }
        public virtual DbSet<NueAccessMapper> NueAccessMapper { get; set; }
        public virtual DbSet<NueAccessMaster> NueAccessMaster { get; set; }
        public virtual DbSet<NueAddressProofRequest> NueAddressProofRequest { get; set; }
        public virtual DbSet<NueDblocationChangeRequest> NueDblocationChangeRequest { get; set; }
        public virtual DbSet<NueDbmanagerChangeRequest> NueDbmanagerChangeRequest { get; set; }
        public virtual DbSet<NueDomesticTripRequest> NueDomesticTripRequest { get; set; }
        public virtual DbSet<NueGeneralRequest> NueGeneralRequest { get; set; }
        public virtual DbSet<NueGoalAccessMapper> NueGoalAccessMapper { get; set; }
        public virtual DbSet<NueGoalAccessType> NueGoalAccessType { get; set; }
        public virtual DbSet<NueGoalCatgoryTypeMaster> NueGoalCatgoryTypeMaster { get; set; }
        public virtual DbSet<NueGoalGlobelRepo> NueGoalGlobelRepo { get; set; }
        public virtual DbSet<NueGoalLocalRepo> NueGoalLocalRepo { get; set; }
        public virtual DbSet<NueGoalStatusMapper> NueGoalStatusMapper { get; set; }
        public virtual DbSet<NueGoalStatusTypeMaster> NueGoalStatusTypeMaster { get; set; }
        public virtual DbSet<NueInternationalTripRequest> NueInternationalTripRequest { get; set; }
        public virtual DbSet<NueLeaveBalanceEnquiryRequest> NueLeaveBalanceEnquiryRequest { get; set; }
        public virtual DbSet<NueLeaveCancelationRequest> NueLeaveCancelationRequest { get; set; }
        public virtual DbSet<NueLeavePastApplyRequest> NueLeavePastApplyRequest { get; set; }
        public virtual DbSet<NueLeaveWfhapplyRequest> NueLeaveWfhapplyRequest { get; set; }
        public virtual DbSet<NueManagerMapper> NueManagerMapper { get; set; }
        public virtual DbSet<NuePgbrequest> NuePgbrequest { get; set; }
        public virtual DbSet<NuePgbrequestUsers> NuePgbrequestUsers { get; set; }
        public virtual DbSet<NueRequestAceessLog> NueRequestAceessLog { get; set; }
        public virtual DbSet<NueRequestActivity> NueRequestActivity { get; set; }
        public virtual DbSet<NueRequestActivityMaster> NueRequestActivityMaster { get; set; }
        public virtual DbSet<NueRequestAttachmentLog> NueRequestAttachmentLog { get; set; }
        public virtual DbSet<NueRequestMaster> NueRequestMaster { get; set; }
        public virtual DbSet<NueRequestStatus> NueRequestStatus { get; set; }
        public virtual DbSet<NueRequestSubType> NueRequestSubType { get; set; }
        public virtual DbSet<NueRequestType> NueRequestType { get; set; }
        public virtual DbSet<NueSalaryCertificateRequest> NueSalaryCertificateRequest { get; set; }
        public virtual DbSet<NueUserOrgMapper> NueUserOrgMapper { get; set; }
        public virtual DbSet<NueUserProfile> NueUserProfile { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<State> State { get; set; }

        // Unable to generate entity type for table 'HangFire.Counter'. Please see the warning messages.

        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Server=MJOSE01\\TEST;Database=NueRequest;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer(GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("PK_HangFire_CounterAggregated");

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_AggregatedCounter_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field })
                    .HasName("PK_HangFire_Hash");

                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Hash_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.StateName)
                    .HasName("IX_HangFire_Job_StateName")
                    .HasFilter("([StateName] IS NOT NULL)");

                entity.HasIndex(e => new { e.StateName, e.ExpireAt })
                    .HasName("IX_HangFire_Job_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name })
                    .HasName("PK_HangFire_JobParameter");

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameter)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id })
                    .HasName("PK_HangFire_JobQueue");

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id })
                    .HasName("PK_HangFire_List");

                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_List_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<MichaelApproverStatusTypes>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(2500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MichaelDepartmentMaster>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Departmentname)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelDepartmentMaster)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelDe__UserI__49B9D516");
            });

            modelBuilder.Entity<MichaelDepartmentRequestMaster>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestTypeDescription).HasColumnType("text");

                entity.Property(e => e.RequestTypeName).HasColumnType("text");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MichaelDepartmentRequestMaster)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__MichaelDe__Depar__3FFB60B2");

                entity.HasOne(d => d.RequestPriority)
                    .WithMany(p => p.MichaelDepartmentRequestMaster)
                    .HasForeignKey(d => d.RequestPriorityId)
                    .HasConstraintName("FK__MichaelDe__Reque__40EF84EB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelDepartmentRequestMaster)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelDe__UserI__3F073C79");
            });

            modelBuilder.Entity<MichaelEscalationBase>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.EscalationLevel)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MichaelPayloadDataType>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.DataType)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MichaelRequestAccessMapper>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.RequestAccessTypes)
                    .WithMany(p => p.MichaelRequestAccessMapper)
                    .HasForeignKey(d => d.RequestAccessTypesId)
                    .HasConstraintName("FK__MichaelRe__Reque__7192BC46");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.MichaelRequestAccessMapper)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__MichaelRe__Reque__6FAA73D4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestAccessMapper)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__709E980D");
            });

            modelBuilder.Entity<MichaelRequestAccessTypes>(entity =>
            {
                entity.Property(e => e.AccessTypes)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MichaelRequestApproverStatusMapper>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.ApproverStatus)
                    .WithMany(p => p.MichaelRequestApproverStatusMapper)
                    .HasForeignKey(d => d.ApproverStatusId)
                    .HasConstraintName("FK__MichaelRe__Appro__7933DE0E");

                entity.HasOne(d => d.RequestAccess)
                    .WithMany(p => p.MichaelRequestApproverStatusMapper)
                    .HasForeignKey(d => d.RequestAccessId)
                    .HasConstraintName("FK__MichaelRe__Reque__774B959C");

                entity.HasOne(d => d.RequestAccessTypes)
                    .WithMany(p => p.MichaelRequestApproverStatusMapper)
                    .HasForeignKey(d => d.RequestAccessTypesId)
                    .HasConstraintName("FK__MichaelRe__Reque__783FB9D5");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.MichaelRequestApproverStatusMapper)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__MichaelRe__Reque__75634D2A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestApproverStatusMapper)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__76577163");
            });

            modelBuilder.Entity<MichaelRequestAttachmentLog>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.FileExt).HasColumnType("text");

                entity.Property(e => e.FileName).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.Property(e => e.VfileName)
                    .HasColumnName("VFileName")
                    .HasColumnType("text");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.MichaelRequestAttachmentLog)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__MichaelRe__Reque__04A590BA");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestAttachmentLog)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__0599B4F3");
            });

            modelBuilder.Entity<MichaelRequestEscalationAccessLogs>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.RequestEscalationMapper)
                    .WithMany(p => p.MichaelRequestEscalationAccessLogs)
                    .HasForeignKey(d => d.RequestEscalationMapperId)
                    .HasConstraintName("FK__MichaelRe__Reque__615C547D");

                entity.HasOne(d => d.RequestEscalationUser)
                    .WithMany(p => p.MichaelRequestEscalationAccessLogs)
                    .HasForeignKey(d => d.RequestEscalationUserId)
                    .HasConstraintName("FK__MichaelRe__Reque__625078B6");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.MichaelRequestEscalationAccessLogs)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__MichaelRe__Reque__5F740C0B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestEscalationAccessLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__60683044");
            });

            modelBuilder.Entity<MichaelRequestEscalationDurationLogs>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.RequestEscalationMapper)
                    .WithMany(p => p.MichaelRequestEscalationDurationLogs)
                    .HasForeignKey(d => d.RequestEscalationMapperId)
                    .HasConstraintName("FK__MichaelRe__Reque__67152DD3");

                entity.HasOne(d => d.RequestEscalationUser)
                    .WithMany(p => p.MichaelRequestEscalationDurationLogs)
                    .HasForeignKey(d => d.RequestEscalationUserId)
                    .HasConstraintName("FK__MichaelRe__Reque__6809520C");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.MichaelRequestEscalationDurationLogs)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__MichaelRe__Reque__652CE561");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestEscalationDurationLogs)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__6621099A");
            });

            modelBuilder.Entity<MichaelRequestEscalationMapper>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Level).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MichaelRequestEscalationMapper)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__MichaelRe__Depar__46A85E41");

                entity.HasOne(d => d.DepartmentRequest)
                    .WithMany(p => p.MichaelRequestEscalationMapper)
                    .HasForeignKey(d => d.DepartmentRequestId)
                    .HasConstraintName("FK__MichaelRe__Depar__479C827A");

                entity.HasOne(d => d.EscalationBase)
                    .WithMany(p => p.MichaelRequestEscalationMapper)
                    .HasForeignKey(d => d.EscalationBaseId)
                    .HasConstraintName("FK__MichaelRe__Escal__4890A6B3");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestEscalationMapper)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__45B43A08");
            });

            modelBuilder.Entity<MichaelRequestEscalationUserBaseMapper>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.RequestEscalationMapper)
                    .WithMany(p => p.MichaelRequestEscalationUserBaseMapper)
                    .HasForeignKey(d => d.RequestEscalationMapperId)
                    .HasConstraintName("FK__MichaelRe__Reque__4D555BD0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestEscalationUserBaseMapper)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__4C613797");
            });

            modelBuilder.Entity<MichaelRequestLog>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.RequestEscalationMapper)
                    .WithMany(p => p.MichaelRequestLog)
                    .HasForeignKey(d => d.RequestEscalationMapperId)
                    .HasConstraintName("FK__MichaelRe__Reque__7FE0DB9D");

                entity.HasOne(d => d.RequestEscalationUser)
                    .WithMany(p => p.MichaelRequestLog)
                    .HasForeignKey(d => d.RequestEscalationUserId)
                    .HasConstraintName("FK__MichaelRe__Reque__00D4FFD6");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.MichaelRequestLog)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__MichaelRe__Reque__7C104AB9");

                entity.HasOne(d => d.RequestLogType)
                    .WithMany(p => p.MichaelRequestLog)
                    .HasForeignKey(d => d.RequestLogTypeId)
                    .HasConstraintName("FK__MichaelRe__Reque__7DF8932B");

                entity.HasOne(d => d.RequestPriority)
                    .WithMany(p => p.MichaelRequestLog)
                    .HasForeignKey(d => d.RequestPriorityId)
                    .HasConstraintName("FK__MichaelRe__Reque__01C9240F");

                entity.HasOne(d => d.RequestStageBase)
                    .WithMany(p => p.MichaelRequestLog)
                    .HasForeignKey(d => d.RequestStageBaseId)
                    .HasConstraintName("FK__MichaelRe__Reque__7EECB764");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestLog)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__7D046EF2");
            });

            modelBuilder.Entity<MichaelRequestLogTypes>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.LogType)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<MichaelRequestMaster>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.IsApprovalProcess).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsApprovalProcessComplted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.MichaelRequestMaster)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK__MichaelRe__Depar__530E3526");

                entity.HasOne(d => d.DepartmentRequest)
                    .WithMany(p => p.MichaelRequestMaster)
                    .HasForeignKey(d => d.DepartmentRequestId)
                    .HasConstraintName("FK__MichaelRe__Depar__5402595F");

                entity.HasOne(d => d.RequestEscalationMapper)
                    .WithMany(p => p.MichaelRequestMaster)
                    .HasForeignKey(d => d.RequestEscalationMapperId)
                    .HasConstraintName("FK__MichaelRe__Reque__55EAA1D1");

                entity.HasOne(d => d.RequestEscalationUser)
                    .WithMany(p => p.MichaelRequestMaster)
                    .HasForeignKey(d => d.RequestEscalationUserId)
                    .HasConstraintName("FK__MichaelRe__Reque__56DEC60A");

                entity.HasOne(d => d.RequestPriority)
                    .WithMany(p => p.MichaelRequestMaster)
                    .HasForeignKey(d => d.RequestPriorityId)
                    .HasConstraintName("FK__MichaelRe__Reque__57D2EA43");

                entity.HasOne(d => d.RequestStageBase)
                    .WithMany(p => p.MichaelRequestMaster)
                    .HasForeignKey(d => d.RequestStageBaseId)
                    .HasConstraintName("FK__MichaelRe__Reque__54F67D98");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MichaelRequestMaster)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MichaelRe__UserI__521A10ED");
            });

            modelBuilder.Entity<MichaelRequestPayloadMaster>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.PayloadDataTypeNavigation)
                    .WithMany(p => p.MichaelRequestPayloadMaster)
                    .HasForeignKey(d => d.PayloadDataType)
                    .HasConstraintName("FK__MichaelRe__Paylo__5BA37B27");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.MichaelRequestPayloadMaster)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__MichaelRe__Reque__5AAF56EE");
            });

            modelBuilder.Entity<MichaelRequestPriority>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestPriority)
                    .HasMaxLength(2500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MichaelRequestStageBase>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StageType)
                    .HasMaxLength(2500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MichaelRequestStageLogs>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.MichaelRequestStageLogs)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__MichaelRe__Reque__6AE5BEB7");

                entity.HasOne(d => d.RequestStageBase)
                    .WithMany(p => p.MichaelRequestStageLogs)
                    .HasForeignKey(d => d.RequestStageBaseId)
                    .HasConstraintName("FK__MichaelRe__Reque__6BD9E2F0");
            });

            modelBuilder.Entity<NeuCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("PK__NeuCount__10D160BFD7D66FFA");

                entity.Property(e => e.CountryId)
                    .HasColumnName("CountryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ThreeCharCountryCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.TwoCharCountryCode)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NeuDesignation>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Desig)
                    .HasMaxLength(450)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NeuEmployeeVerificationRequest>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NeuEmployeeVerificationRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__EmployeeV__UserI__73A521EA");
            });

            modelBuilder.Entity<NeuEmploymentStatus>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasMaxLength(450)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NeuJobLevel>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Level)
                    .HasColumnName("LEVEL")
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NeuMessages>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK_Messages");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Message).HasMaxLength(2500);

                entity.Property(e => e.Processed).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NeuMessages)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Messages__UserId__0C1BC9F9");
            });

            modelBuilder.Entity<NeuPractice>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Practice)
                    .HasMaxLength(450)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NeuUserDetail>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.City)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Company)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.Extension)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.HomePhone)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LoginName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LoginNameWithDomain)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Manager)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ManagerEmail)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(450)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NeuUserPreference>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.IsMailCommunication).HasDefaultValueSql("((1))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.FirstApproverNavigation)
                    .WithMany(p => p.NeuUserPreferenceFirstApproverNavigation)
                    .HasForeignKey(d => d.FirstApprover)
                    .HasConstraintName("FK__NeuUserPr__First__0EE3280B");

                entity.HasOne(d => d.SecondApproverNavigation)
                    .WithMany(p => p.NeuUserPreferenceSecondApproverNavigation)
                    .HasForeignKey(d => d.SecondApprover)
                    .HasConstraintName("FK__NeuUserPr__Secon__0FD74C44");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NeuUserPreferenceUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__UserPrefe__UserI__7187CF4E");
            });

            modelBuilder.Entity<NueAccessMapper>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Access)
                    .WithMany(p => p.NueAccessMapper)
                    .HasForeignKey(d => d.AccessId)
                    .HasConstraintName("FK__NueAccess__Acces__5441852A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueAccessMapper)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueAccess__UserI__534D60F1");
            });

            modelBuilder.Entity<NueAccessMaster>(entity =>
            {
                entity.Property(e => e.AccessDesc)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<NueAddressProofRequest>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueAddressProofRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueAddres__UserI__6DEC4894");
            });

            modelBuilder.Entity<NueDblocationChangeRequest>(entity =>
            {
                entity.ToTable("NueDBLocationChangeRequest");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Location).HasColumnType("text");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueDblocationChangeRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueDBLoca__UserI__33AA9866");
            });

            modelBuilder.Entity<NueDbmanagerChangeRequest>(entity =>
            {
                entity.ToTable("NueDBManagerChangeRequest");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.ProjectName).HasColumnType("text");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.NueDbmanagerChangeRequestManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__NueDBMana__Manag__08C03A61");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueDbmanagerChangeRequestUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueDBMana__UserI__07CC1628");
            });

            modelBuilder.Entity<NueDomesticTripRequest>(entity =>
            {
                entity.Property(e => e.Accommodation).HasDefaultValueSql("((0))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LocationFrom).HasColumnType("text");

                entity.Property(e => e.LocationTo).HasColumnType("text");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueDomesticTripRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueDomest__UserI__2B7F66B9");
            });

            modelBuilder.Entity<NueGeneralRequest>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueGeneralRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueGenera__UserI__04CFADEC");
            });

            modelBuilder.Entity<NueGoalAccessMapper>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Temp).HasColumnType("text");

                entity.HasOne(d => d.GoalAccessType)
                    .WithMany(p => p.NueGoalAccessMapper)
                    .HasForeignKey(d => d.GoalAccessTypeId)
                    .HasConstraintName("FK__NueGoalAc__GoalA__4EB3945D");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.NueGoalAccessMapper)
                    .HasForeignKey(d => d.GoalId)
                    .HasConstraintName("FK__NueGoalAc__GoalI__4CCB4BEB");

                entity.HasOne(d => d.GoalType)
                    .WithMany(p => p.NueGoalAccessMapper)
                    .HasForeignKey(d => d.GoalTypeId)
                    .HasConstraintName("FK__NueGoalAc__GoalT__4DBF7024");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.NueGoalAccessMapperOwner)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__NueGoalAc__Owner__4BD727B2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueGoalAccessMapperUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueGoalAc__UserI__4AE30379");
            });

            modelBuilder.Entity<NueGoalAccessType>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.GoalAccessType)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<NueGoalCatgoryTypeMaster>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.GoalDesc)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Temp).HasColumnType("text");
            });

            modelBuilder.Entity<NueGoalGlobelRepo>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Desc).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.HasOne(d => d.GoalType)
                    .WithMany(p => p.NueGoalGlobelRepo)
                    .HasForeignKey(d => d.GoalTypeId)
                    .HasConstraintName("FK__NueGoalGl__GoalT__3F7150CD");

                entity.HasOne(d => d.LocalRepo)
                    .WithMany(p => p.NueGoalGlobelRepo)
                    .HasForeignKey(d => d.LocalRepoId)
                    .HasConstraintName("FK__NueGoalGl__Local__3E7D2C94");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueGoalGlobelRepo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueGoalGl__UserI__3D89085B");
            });

            modelBuilder.Entity<NueGoalLocalRepo>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.GoalDesc).HasColumnType("text");

                entity.Property(e => e.GoalTitle)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.GoalCatTypeNavigation)
                    .WithMany(p => p.NueGoalLocalRepo)
                    .HasForeignKey(d => d.GoalCatType)
                    .HasConstraintName("FK__NueGoalLo__GoalC__47127295");

                entity.HasOne(d => d.GoalType)
                    .WithMany(p => p.NueGoalLocalRepo)
                    .HasForeignKey(d => d.GoalTypeId)
                    .HasConstraintName("FK__NueGoalLo__GoalT__38C4533E");

                entity.HasOne(d => d.InitiOwnerNavigation)
                    .WithMany(p => p.NueGoalLocalRepoInitiOwnerNavigation)
                    .HasForeignKey(d => d.InitiOwner)
                    .HasConstraintName("FK__NueGoalLo__Initi__37D02F05");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK__NueGoalLo__Paren__3AAC9BB0");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueGoalLocalRepoUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueGoalLo__UserI__36DC0ACC");
            });

            modelBuilder.Entity<NueGoalStatusMapper>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.GoalEndDate).HasColumnType("date");

                entity.Property(e => e.GoalStartDate).HasColumnType("date");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Temp).HasColumnType("text");

                entity.HasOne(d => d.Goal)
                    .WithMany(p => p.NueGoalStatusMapper)
                    .HasForeignKey(d => d.GoalId)
                    .HasConstraintName("FK__NueGoalSt__GoalI__452A2A23");

                entity.HasOne(d => d.GoalStatus)
                    .WithMany(p => p.NueGoalStatusMapper)
                    .HasForeignKey(d => d.GoalStatusId)
                    .HasConstraintName("FK__NueGoalSt__GoalS__480696CE");

                entity.HasOne(d => d.GoalType)
                    .WithMany(p => p.NueGoalStatusMapper)
                    .HasForeignKey(d => d.GoalTypeId)
                    .HasConstraintName("FK__NueGoalSt__GoalT__461E4E5C");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.NueGoalStatusMapperOwner)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__NueGoalSt__Owner__443605EA");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueGoalStatusMapperUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueGoalSt__UserI__4341E1B1");
            });

            modelBuilder.Entity<NueGoalStatusTypeMaster>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.StausDesc)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Temp).HasColumnType("text");
            });

            modelBuilder.Entity<NueInternationalTripRequest>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.NeedVisiaProcessing).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlaceToVisit).HasColumnType("text");

                entity.Property(e => e.ProjectName).HasColumnType("text");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueInternationalTripRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueIntern__UserI__37E53D9E");
            });

            modelBuilder.Entity<NueLeaveBalanceEnquiryRequest>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueLeaveBalanceEnquiryRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueLeaveB__UserI__59E54FE7");
            });

            modelBuilder.Entity<NueLeaveCancelationRequest>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueLeaveCancelationRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueLeaveC__UserI__3587F3E0");
            });

            modelBuilder.Entity<NueLeavePastApplyRequest>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueLeavePastApplyRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueLeaveP__UserI__18178C8A");
            });

            modelBuilder.Entity<NueLeaveWfhapplyRequest>(entity =>
            {
                entity.ToTable("NueLeaveWFHApplyRequest");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueLeaveWfhapplyRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueLeaveW__UserI__31D75E8D");
            });

            modelBuilder.Entity<NueManagerMapper>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsConsultentManager).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.NueManagerMapperManager)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK__NueManage__Manag__395884C4");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueManagerMapperUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueManage__UserI__3864608B");
            });

            modelBuilder.Entity<NuePgbrequest>(entity =>
            {
                entity.ToTable("NuePGBRequest");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.EstimatedRevenue)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.OpMode)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.Property(e => e.StartFinancialQuarter)
                    .HasMaxLength(2500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.NuePgbrequest)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK__NuePGBReq__Count__7D197D8B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NuePgbrequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NuePGBReq__UserI__7E0DA1C4");
            });

            modelBuilder.Entity<NuePgbrequestUsers>(entity =>
            {
                entity.ToTable("NuePGBRequestUsers");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.PgbrequestId).HasColumnName("PGBRequestId");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.Temp).HasColumnType("text");

                entity.HasOne(d => d.Pgbrequest)
                    .WithMany(p => p.NuePgbrequestUsers)
                    .HasForeignKey(d => d.PgbrequestId)
                    .HasConstraintName("FK__NuePGBReq__PGBRe__04BA9F53");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NuePgbrequestUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NuePGBReq__UserI__03C67B1A");
            });

            modelBuilder.Entity<NueRequestAceessLog>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Completed).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.NueRequestAceessLogOwner)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__NueReques__Owner__32AB8735");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.NueRequestAceessLog)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__NueReques__Reque__2FCF1A8A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueRequestAceessLogUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueReques__UserI__30C33EC3");
            });

            modelBuilder.Entity<NueRequestActivity>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Payload).HasColumnType("text");

                entity.Property(e => e.Request)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.PayloadTypeNavigation)
                    .WithMany(p => p.NueRequestActivity)
                    .HasForeignKey(d => d.PayloadType)
                    .HasConstraintName("FK__NueReques__Paylo__671F4F74");

                entity.HasOne(d => d.RequestNavigation)
                    .WithMany(p => p.NueRequestActivity)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__NueReques__Reque__690797E6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueRequestActivity)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueReques__UserI__681373AD");
            });

            modelBuilder.Entity<NueRequestActivityMaster>(entity =>
            {
                entity.Property(e => e.ActivityDesc)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<NueRequestAttachmentLog>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.FileExt)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.FileName)
                    .HasMaxLength(1050)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.Request)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.Property(e => e.VfileName)
                    .HasColumnName("VFileName")
                    .HasMaxLength(2050)
                    .IsUnicode(false);

                entity.HasOne(d => d.Owner)
                    .WithMany(p => p.NueRequestAttachmentLogOwner)
                    .HasForeignKey(d => d.OwnerId)
                    .HasConstraintName("FK__NueReques__Owner__72910220");

                entity.HasOne(d => d.RequestNavigation)
                    .WithMany(p => p.NueRequestAttachmentLog)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__NueReques__Reque__70A8B9AE");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueRequestAttachmentLogUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueReques__UserI__719CDDE7");
            });

            modelBuilder.Entity<NueRequestMaster>(entity =>
            {
                entity.HasIndex(e => new { e.RequestId, e.Id })
                    .HasName("UniqueRequestId")
                    .IsUnique();

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.IsApprovalProcess).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.NueRequestMaster)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK__NueReques__Creat__2B0A656D");

                entity.HasOne(d => d.RequestCatTypeNavigation)
                    .WithMany(p => p.NueRequestMaster)
                    .HasForeignKey(d => d.RequestCatType)
                    .HasConstraintName("FK__NueReques__Reque__2CF2ADDF");

                entity.HasOne(d => d.RequestStatusNavigation)
                    .WithMany(p => p.NueRequestMaster)
                    .HasForeignKey(d => d.RequestStatus)
                    .HasConstraintName("FK__NueReques__Reque__2BFE89A6");
            });

            modelBuilder.Entity<NueRequestStatus>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RequestStatus)
                    .HasMaxLength(350)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NueRequestSubType>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RequestSubType)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.RequestTypeNavigation)
                    .WithMany(p => p.NueRequestSubType)
                    .HasForeignKey(d => d.RequestType)
                    .HasConstraintName("FK__NueReques__Reque__5EBF139D");
            });

            modelBuilder.Entity<NueRequestType>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RequestType)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NueSalaryCertificateRequest>(entity =>
            {
                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.Property(e => e.RequestId)
                    .HasMaxLength(350)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueSalaryCertificateRequest)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueSalary__UserI__7F16D496");
            });

            modelBuilder.Entity<NueUserOrgMapper>(entity =>
            {
                entity.Property(e => e.AddedOn)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.OrgUser)
                    .WithMany(p => p.NueUserOrgMapperOrgUser)
                    .HasForeignKey(d => d.OrgUserId)
                    .HasConstraintName("FK__NueUserOr__OrgUs__72C60C4A");

                entity.HasOne(d => d.OrgUserTypeNavigation)
                    .WithMany(p => p.NueUserOrgMapper)
                    .HasForeignKey(d => d.OrgUserType)
                    .HasConstraintName("FK__NueUserOr__OrgUs__73BA3083");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NueUserOrgMapperUser)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__NueUserOr__UserI__71D1E811");
            });

            modelBuilder.Entity<NueUserProfile>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.AddedOn).HasColumnType("datetime");

                entity.Property(e => e.DateofJoining)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Ntplid)
                    .HasColumnName("NTPLID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.DesignationNavigation)
                    .WithMany(p => p.NueUserProfile)
                    .HasForeignKey(d => d.Designation)
                    .HasConstraintName("FK__NueUserPr__Desig__44CA3770");

                entity.HasOne(d => d.EmploymentStatusNavigation)
                    .WithMany(p => p.NueUserProfile)
                    .HasForeignKey(d => d.EmploymentStatus)
                    .HasConstraintName("FK__NueUserPr__Emplo__45BE5BA9");

                entity.HasOne(d => d.JobLevelNavigation)
                    .WithMany(p => p.NueUserProfile)
                    .HasForeignKey(d => d.JobLevel)
                    .HasConstraintName("FK__NueUserPr__JobLe__46B27FE2");

                entity.HasOne(d => d.PracticeNavigation)
                    .WithMany(p => p.NueUserProfile)
                    .HasForeignKey(d => d.Practice)
                    .HasConstraintName("FK__NueUserPr__Pract__47A6A41B");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.HasIndex(e => e.LastHeartbeat)
                    .HasName("IX_HangFire_Server_LastHeartbeat");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value })
                    .HasName("PK_HangFire_Set");

                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt)
                    .HasName("IX_HangFire_Set_ExpireAt")
                    .HasFilter("([ExpireAt] IS NOT NULL)");

                entity.HasIndex(e => new { e.Key, e.Score })
                    .HasName("IX_HangFire_Set_Score");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id })
                    .HasName("PK_HangFire_State");

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });
        }
    }
}
