using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GopherSharp {
	// Due to the simple nature of gopher, (no keepalive and other adv.
	// stuff) instead of a Client, we have a static request library.
	// This makes it easier to quickly fetch, as there is no client
	// object needed - Gopher wouldn't need a complex object.
	public static class GopherRequester {
		// TODO: Querying versions of functions below
		// TODO: bytestream version of the Request function
		// TODO: Maybe split the item list parsing into a seperate func
		public static string RequestRaw(string server, string resource, int port = 70) {
			TcpClient tc = new TcpClient(server, port);
			using (Stream s = tc.GetStream()) {
				StreamReader sr = new StreamReader(s);
				StreamWriter sw = new StreamWriter(s);
				sw.AutoFlush = true;
				
				sw.WriteLine(resource);
				
				return sr.ReadToEnd();
			}
		}
		
		public static List<GopherItem> Request(string server, string resource, int port = 70) {
			TcpClient tc = new TcpClient(server, port);
			string buffer = String.Empty;
			using (Stream s = tc.GetStream()) {
				StreamReader sr = new StreamReader(s);
				StreamWriter sw = new StreamWriter(s);
				sw.AutoFlush = true;
				
				sw.WriteLine(resource);
				buffer = sr.ReadToEnd();
			}
			
			string[] bufferItems = Regex.Split(buffer, "\r\n");
			List<GopherItem> items = new List<GopherItem>();
			
			foreach (string s in bufferItems) {
				if (s == ".")
					break;
				items.Add(new GopherItem(s));
			}
			return items;
		}
	}
	
	public class GopherItem {
		public char ItemType {
			get; set;
		}
		
		public string DisplayString {
			get; set;
		}
		public string Selector {
			get; set;
		}
		
		public string Hostname {
			get; set;
		}
		
		public int Port {
			get; set;
		}
		
		public GopherItem() {
			// create an empty information item
			ItemType = 'i';
			DisplayString = String.Empty;
			Selector = String.Empty;
			Hostname = "error.host"; // floodgap does this, correct?
			Port = 1;
		}
		
		public GopherItem(string raw) {
			string parsing = raw;
			// Get the item type
			ItemType = parsing.ToCharArray()[0];
			parsing = parsing.Remove(0, 1);
			// Split the strings
			string[] items = parsing.Split('\t');
			DisplayString = items[0];
			Selector = items[1];
			Hostname = items[2];
			Port = Convert.ToInt32(items[3]);
		}
	}
}
