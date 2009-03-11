﻿// ****************************************************************************
// 
// Copyright (C) 2005-2009  Doom9 & al
// 
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
// 
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace MeGUI
{
    public class HDStreamsExJob : Job
    {
        private string featureNb;
        private string args;

        public HDStreamsExJob()
            : base()
        {
            featureNb = "";
            args = "";
        }

        public HDStreamsExJob(string input, string ouput, string featureNb, string args)
        {
            Input = input;
            Output = ouput;
            FeatureNb = featureNb;
            Args = args;
        }

        public string FeatureNb
        {
            get { return featureNb; }
            set { featureNb = value; }
        }

        public string Args
        {
            get { return args; }
            set { args = value; }
        }

        public override string CodecString
        {
            get { return ""; }
        }

        public override string EncodingMode
        {
            get { return "eac3to"; }
        }
    }
}