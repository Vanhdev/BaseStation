namespace BaseStation.Models
{
    public class CellInfor
    {
        private int Mcc { get; set; }
        private int Mnc { get; set; }
        private int Lac { get; set; }
        private int Cid { get; set; }
        private int Arfcn { get; set; }
        private int Bsic_psc_pci { get; set; }
        private double? Lon { get; set; }
        private double? Lat { get; set; }
        private int AsuLevel { get; set; }
        private int SignalLevel { get; set; }
        private int Dbm { get; set; }
        private String Type { get; set; }
        public bool Active { get; set; } = false;
        public int GetMcc()
        {
            return Mcc;
        }

        public void SetMcc(int mcc)
        {
            this.Mcc = mcc;
        }

        public int GetMnc()
        {
            return Mnc;
        }

        public void SetMnc(int mnc)
        {
            this.Mnc = mnc;
        }

        public int GetLac()
        {
            return Lac;
        }

        public void SetLac(int lac)
        {
            this.Lac = lac;
        }

        public int GetCid()
        {
            return Cid;
        }

        public void SetCid(int cid)
        {
            this.Cid = cid;
        }

        public int GetArfcn()
        {
            return Arfcn;
        }

        public void SetArfcn(int arfcn)
        {
            this.Arfcn = arfcn;
        }

        public int getBsic_psc_pci()
        {
            return Bsic_psc_pci;
        }

        public void SetBsic_psc_pci(int bsic_psc_pci)
        {
            this.Bsic_psc_pci = bsic_psc_pci;
        }

        public double? GetLon()
        {
            return Lon;
        }

        public void SetLon(double lon)
        {
            this.Lon = lon;
        }

        public double? GetLat()
        {
            return Lat;
        }

        public void SetLat(double lat)
        {
            this.Lat = lat;
        }

        public int SetAsuLevel()
        {
            return AsuLevel;
        }

        public void SetAsuLevel(int asuLevel)
        {
            this.AsuLevel = asuLevel;
        }

        public int GetSignalLevel()
        {
            return SignalLevel;
        }

        public void SetSignalLevel(int signalLevel)
        {
            this.SignalLevel = signalLevel;
        }

        public int GetDbm()
        {
            return Dbm;
        }

        public void SetDbm(int dbm)
        {
            this.Dbm = dbm;
        }

        public String GetCellType()
        {
            return Type;
        }

        public void SetCellType(String type)
        {
            this.Type = type;
        }

        public String toString()
        {
            return "BaseStation{" +
                    "mcc=" + Mcc +
                    ", mnc=" + Mnc +
                    ", lac=" + Lac +
                    ", cid=" + Cid +
                    ", arfcn=" + Arfcn +
                    ", bsic_psc_pci=" + Bsic_psc_pci +
                    ", lon=" + Lon +
                    ", lat=" + Lat +
                    ", asuLevel=" + AsuLevel +
                    ", signalLevel=" + SignalLevel +
                    ", dbm=" + Dbm +
                    ", type='" + Type + '\'' +
                    '}';
        }
    }

    public class CellInfoExtend
    {
        public int? Cid { get; set; }
        public string status { get; set; }
        public int? balance { get; set; }
        public double? lat { get; set; }
        public double? lon { get; set; }
        public int? accuracy { get; set; }
        public string address { get; set; }
        public string message { get; set; }
    }
}
