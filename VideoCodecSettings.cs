// ****************************************************************************
// 
// Copyright (C) 2005  Doom9
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
using System.Xml.Serialization;

namespace MeGUI
{
	/// <summary>
	/// Contains basic codec settings, basically all the settings that are often used by codecs like bitrate, encoding mode, etc.
	/// </summary>
	[XmlInclude(typeof(lavcSettings)), XmlInclude(typeof(x264Settings)), XmlInclude(typeof(snowSettings)), XmlInclude(typeof(xvidSettings)), XmlInclude(typeof(hfyuSettings))]
	public abstract class VideoCodecSettings
	{
		public enum Mode:int {CBR = 0, CQ, twopass1, twopass2, twopassAutomated, threepass1, threepass2, threepass3, threepassAutomated, quality};
        int encodingMode, bitrateQuantizer, keyframeInterval, nbBframes, minQuantizer, maxQuantizer, fourCC,
            parX, parY, maxNumberOfPasses, nbThreads;
		bool turbo, v4mv, qpel, trellis;
		decimal creditsQuantizer;
		private string logfile, customEncoderOptions;
		private Zone[] zones;
        protected VideoCodec codec;
        private string[] fourCCs;

		public VideoCodecSettings()
		{
			logfile = ".stats";
			parX = 0;
			parY = 0;
			customEncoderOptions = "";
			fourCC = 0;
			zones = new Zone[0];
		}
        public VideoCodec Codec
        {
            get { return codec; }
        }
		public int EncodingMode
		{
			get {return encodingMode;}
			set {encodingMode = value;}
		}
		public int BitrateQuantizer
		{
			get {return bitrateQuantizer;}
			set {bitrateQuantizer = value;}
		}
		public int KeyframeInterval
		{
			get {return keyframeInterval;}
			set {keyframeInterval = value;}
		}
		public int NbBframes
		{
			get {return nbBframes;}
			set {nbBframes = value;}
		}
		public int MinQuantizer
		{
			get {return minQuantizer;}
			set {minQuantizer = value;}
		}
		public int MaxQuantizer
		{
			get {return maxQuantizer;}
			set {maxQuantizer = value;}
		}
		public int PARX
		{
			get {return parX;}
			set {parX = value;}
		}
		public int PARY
		{
			get {return parY;}
			set {parY = value;}
		}
		public bool Turbo
		{
			get {return turbo;}
			set {turbo = value;}
		}
		public bool V4MV
		{
			get {return v4mv;}
			set {v4mv = value;}
		}
		public bool QPel
		{
			get {return qpel;}
			set {qpel = value;}
		}
		public bool Trellis
		{
			get {return trellis;}
			set {trellis = value;}
		}
		public decimal CreditsQuantizer
		{
			get {return creditsQuantizer;}
			set {creditsQuantizer = value;}
		}
		/// <summary>
		/// returns the available FourCCs for the codec
		/// </summary>
        public string[] FourCCs
        {
            get { return fourCCs; }
            set { fourCCs = value; }
        }
		/// <summary>
		/// gets / sets the logfile
		/// </summary>
		public string Logfile
		{
			get {return logfile;}
			set {logfile = value;}
		}
		/// <summary>
		/// gets / set custom commandline options for the encoder
		/// </summary>
		public string CustomEncoderOptions
		{
			get {return customEncoderOptions;}
			set {customEncoderOptions = value;}
		}
		/// <summary>
		/// gets / sets which fourcc from the FourCCs array is to be used
		/// </summary>
		public int FourCC
		{
			get {return fourCC;}
			set {fourCC = value;}
		}
		/// <summary>
		/// gets / sets the zones
		/// </summary>
		public Zone[] Zones
		{
			get {return zones;}
			set {zones = value;}
		}
		/// <summary>
        ///  gets / sets the maximum number of passes that can be performed with the current codec
        /// </summary>
        public int MaxNumberOfPasses
        {
            get { return maxNumberOfPasses; }
            set { maxNumberOfPasses = value; }
        }
        /// <summary>
        /// gets / sets the number of encoder threads to be used
        /// </summary>
        public int NbThreads
        {
            get { return nbThreads; }
            set { nbThreads = value; }
        }
		/// <summary>
		/// generates a copy of this object
		/// </summary>
		/// <returns>the codec specific settings of this object</returns>
		public virtual VideoCodecSettings clone()
		{
            // This method is sutable for all known descendants!
            return this.MemberwiseClone() as VideoCodecSettings;
		}

        public virtual bool IsAltered(VideoCodecSettings otherSettings)
        {
            return true;
        }
    }
	public enum ZONEMODE: int {QUANTIZER = 0, WEIGHT};
	public struct Zone
	{
		public int startFrame;
		public int endFrame;
		public ZONEMODE mode;
		public decimal modifier;
	}
}
