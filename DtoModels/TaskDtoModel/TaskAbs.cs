using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace DtoModels
{
    public class TaskAbs
    {
        protected int levelOfImportance;
        protected string type;
        


        public int Id { get; set; }
        public string Content { get; set; }

        public bool IsCompleted { get; set; }
        public string Type
        {
            get { return this.type; }
            set
            {
                if (!this.ValidateType(value))
                {
                    throw new ArgumentException("Incorrect type of task");
                }

                this.type = value;
            }
        }

        public int LevelOfImportance
        {
            get { return this.levelOfImportance; }
           set
            {
                if (value < 1)
                    this.levelOfImportance = 1;
                else if (value > 9)
                    this.levelOfImportance = 9;
                else
                    this.levelOfImportance = value;
            }
        }


        public virtual bool ValidateType(string value)
        {
            if (value != Constants.IndividualTaskType && value != Constants.GroupTaskType)
            {
                return false;
            }

            return true;
        }
    }
}
