using System.Collections.Generic;

namespace MegaTFLT.Models.Edq.Models
{
    public class EdqRequestModel
    {
        private List<NameAddressRequestModel> nameAddressRequestModels;
        private List<CountryCityRequestModel> countryCityRequestModels;
        private List<BicRequestModel> bicRequestModels;
        private List<NarrativeRequestModel> narrativeRequestModels;
        private List<PortRequestModel> portRequestModels;
        private List<GoodsRequestModel> goodsRequestModels;
        public List<NameAddressRequestModel> NameAddressRequestModels
        {
            get { return nameAddressRequestModels ?? (nameAddressRequestModels = new List<NameAddressRequestModel>()); }
            set { nameAddressRequestModels = value; }
        }
        public List<CountryCityRequestModel> CountryCityRequestModels
        {
            get { return countryCityRequestModels ?? (countryCityRequestModels = new List<CountryCityRequestModel>()); }
            set { countryCityRequestModels = value; }
        }
        public List<BicRequestModel> BicRequestModels
        {
            get { return bicRequestModels ?? (bicRequestModels = new List<BicRequestModel>()); }
            set { bicRequestModels = value; }
        }
        public List<NarrativeRequestModel> NarrativeRequestModels
        {
            get { return narrativeRequestModels ?? (narrativeRequestModels = new List<NarrativeRequestModel>()); }
            set { narrativeRequestModels = value; }
        }
        public List<PortRequestModel> PortRequestModels
        {
            get { return portRequestModels ?? (portRequestModels = new List<PortRequestModel>()); }
            set { portRequestModels = value; }
        }
        public List<GoodsRequestModel> GoodsRequestModels
        {
            get { return goodsRequestModels ?? (goodsRequestModels = new List<GoodsRequestModel>()); }
            set { goodsRequestModels = value; }
        }
    }
}