﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Volunteer_WPF.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VolunteerEntities : DbContext
    {
        public VolunteerEntities()
            : base("name=VolunteerEntities3")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Abnormal_event> Abnormal_event { get; set; }
        public virtual DbSet<Application_unit> Application_unit { get; set; }
        public virtual DbSet<Apply_Assessment> Apply_Assessment { get; set; }
        public virtual DbSet<Apply_result> Apply_result { get; set; }
        public virtual DbSet<Expertise> Expertises { get; set; }
        public virtual DbSet<Manpower_apply> Manpower_apply { get; set; }
        public virtual DbSet<Service_period> Service_period { get; set; }
        public virtual DbSet<Volunteer_list> Volunteer_list { get; set; }
        public virtual DbSet<account> accounts { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Activity_photo> Activity_photo { get; set; }
        public virtual DbSet<Activity_Schedule> Activity_Schedule { get; set; }
        public virtual DbSet<Activity_type> Activity_type { get; set; }
        public virtual DbSet<event_category> event_category { get; set; }
        public virtual DbSet<Expertise1> Expertise1 { get; set; }
        public virtual DbSet<Human_assessment> Human_assessment { get; set; }
        public virtual DbSet<Human_assessment_result> Human_assessment_result { get; set; }
        public virtual DbSet<Identity_type> Identity_type { get; set; }
        public virtual DbSet<Leader> Leaders { get; set; }
        public virtual DbSet<Lssuing_unit> Lssuing_unit { get; set; }
        public virtual DbSet<Service_group> Service_group { get; set; }
        public virtual DbSet<Service_period1> Service_period1 { get; set; }
        public virtual DbSet<Shift_schedule> Shift_schedule { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Volunteer_supervision> Volunteer_supervision { get; set; }
        public virtual DbSet<Activity1> Activity1 { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Expertise2> Expertise2 { get; set; }
        public virtual DbSet<Leaders1> Leaders1 { get; set; }
        public virtual DbSet<Service_Group1> Service_Group1 { get; set; }
        public virtual DbSet<Service_period2> Service_period2 { get; set; }
        public virtual DbSet<Sign_up> Sign_up { get; set; }
        public virtual DbSet<Sign_up_Service_period> Sign_up_Service_period { get; set; }
        public virtual DbSet<Volunteer> Volunteer { get; set; }
        public virtual DbSet<Sign_up_interview_period> Sign_up_interview_period { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<Sign_up_questionnaire> Sign_up_questionnaire { get; set; }
    }
}
