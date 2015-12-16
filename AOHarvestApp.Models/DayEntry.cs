using System;
using System.Xml.Linq;

namespace AOHarvestApp.Models
{
    public class DayEntry
    {
        public int Id { get; set; }
        public DateTime SpentAt { get; set; }
        public int UserId { get; set; }
        public string Client { get; set; }
        public string ProjectId { get; set; }
        public string Project { get; set; }
        public string TaskId { get; set; }
        public string Task { get; set; }
        public float Hours { get; set; }
        public float HoursWithoutTimer { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public DayEntry()
        {

        }

        public DayEntry(XElement xmlNode)
        {
            var id = xmlNode.Element("id");
            if (id != null) Id = int.Parse(id.Value);

            var spentAt = xmlNode.Element("spent_at");
            if (spentAt != null) SpentAt = DateTime.Parse(spentAt.Value);

            var userId = xmlNode.Element("user_id");
            if (userId != null) UserId = int.Parse(userId.Value);

            var client = xmlNode.Element("client");
            if (client != null) Client = client.Value;

            var projectId = xmlNode.Element("project_id");
            if (projectId != null) ProjectId = projectId.Value;

            var project = xmlNode.Element("project");
            if (project != null) Project = project.Value;

            var taskId = xmlNode.Element("task_id");
            if (taskId != null) TaskId = taskId.Value;

            var task = xmlNode.Element("task");
            if (task != null) Task = task.Value;

            var hours = xmlNode.Element("hours");
            if (hours != null) Hours = float.Parse(hours.Value);

            var hoursWithoutTimer = xmlNode.Element("hours_without_timer");
            if (hoursWithoutTimer != null)
                HoursWithoutTimer = float.Parse(hoursWithoutTimer.Value);

            var notes = xmlNode.Element("notes");
            if (notes != null) Notes = notes.Value;

            var createdAt = xmlNode.Element("created_at");
            if(createdAt != null) CreatedAt = DateTime.Parse(createdAt.Value);

            var updatedAt = xmlNode.Element("updated_at");
            if (updatedAt != null) UpdatedAt = DateTime.Parse(updatedAt.Value);
        }
    }
}
