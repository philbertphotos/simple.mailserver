﻿#region Header
// Copyright (c) 2013 Hans Wolff
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;

namespace Simple.MailServer.Smtp
{
    public class SmtpSessionInfo
    {
		private static object _lck_sessionIDent = new object();
		private static long _sessionIDent = 0;
		
        public bool HasData { get; set; }
        public SmtpIdentification Identification { get; set; }
        public MailAddressWithParameters MailFrom { get; set; }
        public List<MailAddressWithParameters> Recipients { get; private set; }
        public object Tag { get; set; }

        public DateTime CreatedTimestamp { get; private set; }
        public string SessionIDent { get; private set; }

        public SmtpSessionInfo()
		{
			CreatedTimestamp = DateTime.Now;
			SessionIDent = sessionIDent();

			Identification = new SmtpIdentification();
			Recipients = new List<MailAddressWithParameters>();
		}

		public string sessionIDent()
		{
			string ret;

			lock (_lck_sessionIDent) {
				if (_sessionIDent == long.MaxValue)
					_sessionIDent = 0;
				ret = string.Format("{0:X}{1:X}", DateTime.Now.Ticks, ++_sessionIDent);
			}
			return ret;
		}

        public void Reset()
        {
            HasData = false;
            MailFrom = null;
            Recipients.Clear();
        }
    }
}
