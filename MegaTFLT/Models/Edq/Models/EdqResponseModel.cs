using System.Collections.Generic;

namespace MegaTFLT.Models.Edq.Models
{
    public class EdqResponseModel
    {
        private List<NameAddressResponseModel> nameAddressResponseModels;
        private List<CountryCityResponseModel> countryCityResponseModels;
        private List<BicResponseModel> bicResponseModels;
        private List<NarrativeResponseModel> narrativeResponseModels;
        private List<PortResponseModel> portResponseModels;
        private List<GoodsResponseModel> goodsResponseModels;
        public List<NameAddressResponseModel> NameAddressResponseModels
        {
            get { return nameAddressResponseModels ?? (nameAddressResponseModels = new List<NameAddressResponseModel>()); }
            set { nameAddressResponseModels = value; }
        }
        public List<CountryCityResponseModel> CountryCityResponseModels
        {
            get { return countryCityResponseModels ?? (countryCityResponseModels = new List<CountryCityResponseModel>()); }
            set { countryCityResponseModels = value; }
        }
        public List<BicResponseModel> BicResponseModels
        {
            get { return bicResponseModels ?? (bicResponseModels = new List<BicResponseModel>()); }
            set { bicResponseModels = value; }
        }
        public List<NarrativeResponseModel> NarrativeResponseModels
        {
            get { return narrativeResponseModels ?? (narrativeResponseModels = new List<NarrativeResponseModel>()); }
            set { narrativeResponseModels = value; }
        }
        public List<PortResponseModel> PortResponseModels
        {
            get { return portResponseModels ?? (portResponseModels = new List<PortResponseModel>()); }
            set { portResponseModels = value; }
        }
        public List<GoodsResponseModel> GoodsResponseModels
        {
            get { return goodsResponseModels ?? (goodsResponseModels = new List<GoodsResponseModel>()); }
            set { goodsResponseModels = value; }
        }
    }
}