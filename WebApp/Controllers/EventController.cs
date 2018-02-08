using System;
using System.Threading.Tasks;
using KafkaLib;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IKafkaProducer producer;

        private readonly Random clickEventRand = new Random();

        public EventController(IKafkaProducer producer)
        {
            this.producer = producer;
        }

        [HttpPost]
        public Task PostClickEvent(string data)
        {
            var partition = clickEventRand.Next(0, 2);
            return producer.ProduceAsync(Topics.PostClickEventTopic, null, "Partition: " + partition + ". " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + data, partition);
        }

        [HttpPost]
        public Task PostTextEvent(string data)
        {
            var partition = clickEventRand.Next(0, 2);
            return producer.ProduceAsync(Topics.PostTextEventTopic, null, "Partition: " + partition + ". " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + data, partition);
        }

        [HttpPost]
        public Task PostMouseMoveEvent(string data)
        {
            var partition = clickEventRand.Next(0, 2);
            return producer.ProduceAsync(Topics.PostMouseMoveEvent, null, "Partition: " + partition + ". " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " " + data, partition);
        }
    }
}