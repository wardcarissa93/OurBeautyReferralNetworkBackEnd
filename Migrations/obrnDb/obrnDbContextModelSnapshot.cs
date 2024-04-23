﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OurBeautyReferralNetwork.Models;

#nullable disable

namespace OurBeautyReferralNetwork.Migrations.obrnDb
{
    [DbContext(typeof(obrnDbContext))]
    partial class obrnDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pg_catalog", "azure");
            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "pg_catalog", "pgaadauth");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Appointment", b =>
                {
                    b.Property<int>("PkAppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pkAppointmentID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkAppointmentId"));

                    b.Property<DateOnly>("AppointmentDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly>("AppointmentTime")
                        .HasColumnType("time without time zone");

                    b.Property<string>("FkCustomerId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("fkCustomerID");

                    b.Property<int?>("FkServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("fkServiceID");

                    b.Property<bool>("Referred")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.HasKey("PkAppointmentId")
                        .HasName("Appointment_pkey");

                    b.HasIndex("FkCustomerId");

                    b.HasIndex("FkServiceId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.AppointmentService", b =>
                {
                    b.Property<int>("PkAppointmentServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pkAppointmentServiceID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkAppointmentServiceId"));

                    b.Property<int?>("FkAppointmentId")
                        .HasColumnType("integer")
                        .HasColumnName("fkAppointmentID");

                    b.Property<int?>("FkServiceId")
                        .HasColumnType("integer")
                        .HasColumnName("fkServiceID");

                    b.HasKey("PkAppointmentServiceId")
                        .HasName("AppointmentService_pkey");

                    b.HasIndex("FkAppointmentId");

                    b.ToTable("AppointmentService", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Business", b =>
                {
                    b.Property<string>("PkBusinessId")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("pkBusinessID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("BusinessName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("CommissionPaid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Description")
                        .HasMaxLength(1200)
                        .HasColumnType("character varying(1200)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("InsuranceCompany")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateOnly>("InsuranceExpiryDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateOnly>("RegistrationDate")
                        .HasColumnType("date");

                    b.Property<string>("VerificationDocument")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("PkBusinessId")
                        .HasName("Business_pkey");

                    b.ToTable("Business", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Customer", b =>
                {
                    b.Property<string>("PkCustomerId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("pkCustomerID");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<string>("City")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<bool>("Confirm18")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Photo")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Province")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Qr")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("QR");

                    b.Property<bool>("Vip")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("VIP");

                    b.HasKey("PkCustomerId")
                        .HasName("Customer_pkey");

                    b.HasIndex(new[] { "Email" }, "Customer_Email_key")
                        .IsUnique();

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Discount", b =>
                {
                    b.Property<string>("PkDiscountId")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("pkDiscountID");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("numeric");

                    b.HasKey("PkDiscountId")
                        .HasName("Discount_pkey");

                    b.ToTable("Discount", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.FeeAndCommission", b =>
                {
                    b.Property<string>("PkFeeId")
                        .HasMaxLength(4)
                        .HasColumnType("character varying(4)")
                        .HasColumnName("pkFeeID");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FeeType")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character(12)")
                        .IsFixedLength();

                    b.Property<string>("Frequency")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("character(8)")
                        .IsFixedLength();

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("numeric");

                    b.HasKey("PkFeeId")
                        .HasName("FeeAndCommission_pkey");

                    b.ToTable("FeeAndCommission", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Referral", b =>
                {
                    b.Property<int>("PkReferralId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pkReferralID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkReferralId"));

                    b.Property<string>("FkReferredBusinessId")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("fkReferredBusinessID");

                    b.Property<string>("FkReferredCustomerId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("fkReferredCustomerID");

                    b.Property<string>("FkReferrerBusinessId")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("fkReferrerBusinessID");

                    b.Property<string>("FkReferrerCustomerId")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("fkReferrerCustomerID");

                    b.Property<DateOnly>("ReferralDate")
                        .HasColumnType("date");

                    b.Property<string>("ReferredType")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("character varying(1)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("PkReferralId")
                        .HasName("Referral_pkey");

                    b.HasIndex("FkReferredBusinessId");

                    b.HasIndex("FkReferredCustomerId");

                    b.HasIndex("FkReferrerBusinessId");

                    b.HasIndex("FkReferrerCustomerId");

                    b.ToTable("Referral", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Review", b =>
                {
                    b.Property<int>("PkReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pkReviewId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkReviewId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FkBusinessId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("fkBusinessID");

                    b.Property<string>("FkCustomerId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("fkCustomerID");

                    b.Property<string>("Image")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("Provider")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric");

                    b.Property<DateOnly>("ReviewDate")
                        .HasColumnType("date");

                    b.HasKey("PkReviewId")
                        .HasName("Review_pkey");

                    b.HasIndex("FkBusinessId");

                    b.HasIndex("FkCustomerId");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Reward", b =>
                {
                    b.Property<int>("PkRewardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pkRewardID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkRewardId"));

                    b.Property<int>("FkReferralId")
                        .HasColumnType("integer")
                        .HasColumnName("fkReferralID");

                    b.Property<DateOnly>("IssueDate")
                        .HasColumnType("date");

                    b.Property<decimal>("RewardAmount")
                        .HasColumnType("numeric");

                    b.HasKey("PkRewardId")
                        .HasName("Reward_pkey");

                    b.HasIndex("FkReferralId");

                    b.ToTable("Reward", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Service", b =>
                {
                    b.Property<int>("PkServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pkServiceID");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkServiceId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FkBusinessId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("fkBusinessID");

                    b.Property<string>("FkDiscountId")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)")
                        .HasColumnName("fkDiscountID");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("PkServiceId")
                        .HasName("Service_pkey");

                    b.HasIndex("FkBusinessId");

                    b.HasIndex("FkDiscountId");

                    b.ToTable("Service", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Testimonial", b =>
                {
                    b.Property<int>("PkTestimonialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("pkTestimonialId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PkTestimonialId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FkBusinessId")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("fkBusinessID");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric");

                    b.Property<DateOnly>("TestimonialDate")
                        .HasColumnType("date");

                    b.HasKey("PkTestimonialId")
                        .HasName("Testimonial_pkey");

                    b.HasIndex("FkBusinessId");

                    b.ToTable("Testimonial", (string)null);
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Appointment", b =>
                {
                    b.HasOne("OurBeautyReferralNetwork.Models.Customer", "FkCustomer")
                        .WithMany("Appointments")
                        .HasForeignKey("FkCustomerId")
                        .IsRequired()
                        .HasConstraintName("Appointment_fkCustomerID_fkey");

                    b.HasOne("OurBeautyReferralNetwork.Models.AppointmentService", "FkService")
                        .WithMany("Appointments")
                        .HasForeignKey("FkServiceId")
                        .HasConstraintName("fk_appointment_service_id");

                    b.Navigation("FkCustomer");

                    b.Navigation("FkService");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.AppointmentService", b =>
                {
                    b.HasOne("OurBeautyReferralNetwork.Models.Appointment", "FkAppointment")
                        .WithMany("AppointmentServices")
                        .HasForeignKey("FkAppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_appointment_id");

                    b.Navigation("FkAppointment");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Referral", b =>
                {
                    b.HasOne("OurBeautyReferralNetwork.Models.Business", "FkReferredBusiness")
                        .WithMany("ReferralFkReferredBusinesses")
                        .HasForeignKey("FkReferredBusinessId")
                        .HasConstraintName("Referral_fkReferredBusinessID_fkey");

                    b.HasOne("OurBeautyReferralNetwork.Models.Customer", "FkReferredCustomer")
                        .WithMany("ReferralFkReferredCustomers")
                        .HasForeignKey("FkReferredCustomerId")
                        .HasConstraintName("Referral_fkReferredCustomerID_fkey");

                    b.HasOne("OurBeautyReferralNetwork.Models.Business", "FkReferrerBusiness")
                        .WithMany("ReferralFkReferrerBusinesses")
                        .HasForeignKey("FkReferrerBusinessId")
                        .HasConstraintName("Referral_fkReferrerBusinessID_fkey");

                    b.HasOne("OurBeautyReferralNetwork.Models.Customer", "FkReferrerCustomer")
                        .WithMany("ReferralFkReferrerCustomers")
                        .HasForeignKey("FkReferrerCustomerId")
                        .HasConstraintName("Referral_fkReferrerCustomerID_fkey");

                    b.Navigation("FkReferredBusiness");

                    b.Navigation("FkReferredCustomer");

                    b.Navigation("FkReferrerBusiness");

                    b.Navigation("FkReferrerCustomer");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Review", b =>
                {
                    b.HasOne("OurBeautyReferralNetwork.Models.Business", "FkBusiness")
                        .WithMany("Reviews")
                        .HasForeignKey("FkBusinessId")
                        .IsRequired()
                        .HasConstraintName("Review_fkBusinessID_fkey");

                    b.HasOne("OurBeautyReferralNetwork.Models.Customer", "FkCustomer")
                        .WithMany("Reviews")
                        .HasForeignKey("FkCustomerId")
                        .IsRequired()
                        .HasConstraintName("Review_fkCustomerID_fkey");

                    b.Navigation("FkBusiness");

                    b.Navigation("FkCustomer");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Reward", b =>
                {
                    b.HasOne("OurBeautyReferralNetwork.Models.Referral", "FkReferral")
                        .WithMany("Rewards")
                        .HasForeignKey("FkReferralId")
                        .IsRequired()
                        .HasConstraintName("Reward_fkReferralID_fkey");

                    b.Navigation("FkReferral");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Service", b =>
                {
                    b.HasOne("OurBeautyReferralNetwork.Models.Business", "FkBusiness")
                        .WithMany("Services")
                        .HasForeignKey("FkBusinessId")
                        .IsRequired()
                        .HasConstraintName("Service_fkBusinessID_fkey");

                    b.HasOne("OurBeautyReferralNetwork.Models.Discount", "FkDiscount")
                        .WithMany("Services")
                        .HasForeignKey("FkDiscountId")
                        .IsRequired()
                        .HasConstraintName("Service_fkDiscountID_fkey");

                    b.Navigation("FkBusiness");

                    b.Navigation("FkDiscount");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Testimonial", b =>
                {
                    b.HasOne("OurBeautyReferralNetwork.Models.Business", "FkBusiness")
                        .WithMany("Testimonials")
                        .HasForeignKey("FkBusinessId")
                        .IsRequired()
                        .HasConstraintName("Testimonial_fkBusinessID_fkey");

                    b.Navigation("FkBusiness");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Appointment", b =>
                {
                    b.Navigation("AppointmentServices");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.AppointmentService", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Business", b =>
                {
                    b.Navigation("ReferralFkReferredBusinesses");

                    b.Navigation("ReferralFkReferrerBusinesses");

                    b.Navigation("Reviews");

                    b.Navigation("Services");

                    b.Navigation("Testimonials");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Customer", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("ReferralFkReferredCustomers");

                    b.Navigation("ReferralFkReferrerCustomers");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Discount", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("OurBeautyReferralNetwork.Models.Referral", b =>
                {
                    b.Navigation("Rewards");
                });
#pragma warning restore 612, 618
        }
    }
}
