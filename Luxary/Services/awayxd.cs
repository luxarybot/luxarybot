using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Audio;
using Discord.WebSocket;
using Discord.Commands;
using System.IO;
using System.Net.Http;
using ImageSharp;
using ImageSharp.Drawing;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO.Compression;
using Luxary.Service;

namespace Luxary.Service
{
    public class Away
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string User { get; set; }
        public DateTime? AwayTime { get; set; }
        public void SetMessage(string message)
        {
            if (!(string.IsNullOrEmpty(message)))
            {
                this.Message = message;
            }

        }
        public void ToggleAway()
        {
            if (this.Status)
            {
                this.Status = false;
            }
            else
            {
                this.Status = true;
                this.AwayTime = DateTime.Now;
            }
        }
    }
}
   
