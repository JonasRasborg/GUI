using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ScrumBoard_WEB.Models
{
    public class Task
    {
        private string _name;
        private string _description;
        private int _priority;
        private int _estimatedTime;
        private string _responsible;
        private int id;

        public Task(string name, string description, int priority, int estimatedTime, string responsible)
        {
            Name = name;
            Description = description;
            Priority = priority;
            EstimatedTime = estimatedTime;
            Responsible = responsible;
        }

        public Task()
        {

        }
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        public int EstimatedTime
        {
            get { return _estimatedTime; }
            set { _estimatedTime = value; }
        }

        public string Responsible
        {
            get { return _responsible; }
            set { _responsible = value; }
        }
    }

    public class TaskDBContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
    }
}