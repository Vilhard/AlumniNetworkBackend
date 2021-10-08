﻿// <auto-generated />
using System;
using AlumniNetworkBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AlumniNetworkBackend.Migrations
{
    [DbContext(typeof(AlumniNetworkDbContext))]
    partial class AlumniNetworkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AllowGuests")
                        .HasColumnType("bit");

                    b.Property<string>("BannerImg")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedById")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.EventGroupInvite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("GroupId");

                    b.ToTable("EventGroupInvite");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.EventTopicInvite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("TopicId");

                    b.ToTable("EventTopicInvite");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.EventUserInvite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("EventUserInvite");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("IsPrivate")
                        .HasMaxLength(300)
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.GroupMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("GroupMember");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ReplyParentIdId")
                        .HasColumnType("int");

                    b.Property<int?>("SenderIdId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetEventId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetPostId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetTopicId")
                        .HasColumnType("int");

                    b.Property<int?>("TargetUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReplyParentIdId");

                    b.HasIndex("SenderIdId");

                    b.HasIndex("TargetEventId");

                    b.HasIndex("TargetGroupId");

                    b.HasIndex("TargetPostId");

                    b.HasIndex("TargetTopicId");

                    b.HasIndex("TargetUserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.RSVP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<int>("GuestCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("UserId");

                    b.ToTable("RSVP");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.TopicMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("TopicMember");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FunFact")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Picture")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.Event", b =>
                {
                    b.HasOne("AlumniNetworkBackend.Models.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.EventGroupInvite", b =>
                {
                    b.HasOne("AlumniNetworkBackend.Models.Domain.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.Navigation("Event");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.EventTopicInvite", b =>
                {
                    b.HasOne("AlumniNetworkBackend.Models.Domain.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.Navigation("Event");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.EventUserInvite", b =>
                {
                    b.HasOne("AlumniNetworkBackend.Models.Domain.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.GroupMember", b =>
                {
                    b.HasOne("AlumniNetworkBackend.Models.Domain.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.Post", b =>
                {
                    b.HasOne("AlumniNetworkBackend.Models.Domain.User", "ReplyParentId")
                        .WithMany()
                        .HasForeignKey("ReplyParentIdId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.User", "SenderId")
                        .WithMany()
                        .HasForeignKey("SenderIdId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.Event", "TargetEvent")
                        .WithMany()
                        .HasForeignKey("TargetEventId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.Group", "TargetGroup")
                        .WithMany()
                        .HasForeignKey("TargetGroupId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.Post", "TargetPost")
                        .WithMany()
                        .HasForeignKey("TargetPostId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.Topic", "TargetTopic")
                        .WithMany()
                        .HasForeignKey("TargetTopicId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.User", "TargetUser")
                        .WithMany()
                        .HasForeignKey("TargetUserId");

                    b.Navigation("ReplyParentId");

                    b.Navigation("SenderId");

                    b.Navigation("TargetEvent");

                    b.Navigation("TargetGroup");

                    b.Navigation("TargetPost");

                    b.Navigation("TargetTopic");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.RSVP", b =>
                {
                    b.HasOne("AlumniNetworkBackend.Models.Domain.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Event");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AlumniNetworkBackend.Models.Domain.TopicMember", b =>
                {
                    b.HasOne("AlumniNetworkBackend.Models.Domain.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.HasOne("AlumniNetworkBackend.Models.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Topic");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
