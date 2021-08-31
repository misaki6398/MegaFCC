using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MegaTFLT.MegaEcm.Models;
using MegaTFLT.Models.Edq.Models;
using MegaTFLT.Models.MegaEcm.MapProfiles;
using MegaTFLT.Models.MegaEcm.Models;
using MegaTFLT.Models.MegaEcm.Repositorys;
using MegaTFLT.Services.Parsers;
using MegaTFLT.Utilitys;
using Oracle.ManagedDataAccess.Client;

namespace MegaTFLT.Services.EdqServices
{
    public class EdqService
    {

        private readonly IMapper _mapper;

        public EdqService()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ScreeningInputTagToRequestProfile());
                cfg.AddProfile(new EdqResponseToAlertsProfile());
            });

            _mapper = config.CreateMapper();

        }
        public async Task<List<TfAlertsModel>> ProcessScreeningAsync(Dictionary<string, List<ScreeningInputTagModel>> screeningInputTags, Dictionary<string, List<ScreeningInputTagModel>> screeningInputSubTags)
        {
            EdqRequestModel edqRequestModel = new EdqRequestModel();

            if (screeningInputTags != null)
            {
                var intersectKeys = screeningInputTags.Keys.Intersect(ConfigUtility.ScreenConfigs.Keys);

                foreach (string intersectKey in intersectKeys)
                {
                    this.DispatchData(intersectKey, screeningInputTags, ConfigUtility.ScreenConfigs, edqRequestModel);
                }
            }

            if (screeningInputSubTags != null)
            {
                var intersectKeys = screeningInputSubTags.Keys.Intersect(ConfigUtility.ScreenSubConfigs.Keys);

                foreach (string intersectKey in intersectKeys)
                {
                    this.DispatchData(intersectKey, screeningInputSubTags, ConfigUtility.ScreenSubConfigs, edqRequestModel);
                }
            }

            // Start Screen
            EdqResponseModel edqResponseModel = new EdqResponseModel();
            foreach (var item in edqRequestModel.GetType().GetProperties())
            {
                await EdqServiceFactory.Screen(item.Name, edqRequestModel, edqResponseModel);
            }

            var tfAlertsModels = new List<TfAlertsModel>();
            tfAlertsModels.AddRange(_mapper.Map<List<NameAddressResponseModel>, List<TfAlertsModel>>(edqResponseModel.NameAddressResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<CountryCityResponseModel>, List<TfAlertsModel>>(edqResponseModel.CountryCityResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<GoodsResponseModel>, List<TfAlertsModel>>(edqResponseModel.GoodsResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<NarrativeResponseModel>, List<TfAlertsModel>>(edqResponseModel.NarrativeResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<PortResponseModel>, List<TfAlertsModel>>(edqResponseModel.PortResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<BicResponseModel>, List<TfAlertsModel>>(edqResponseModel.BicResponseModels));

            return tfAlertsModels;
        }
      
        private void DispatchData(string intersectKey,
        Dictionary<string, List<ScreeningInputTagModel>> screeningInputTags,
        Dictionary<string, TfScreenConfigModel> screenConfig,
        EdqRequestModel edqRequestModel)
        {

            if (screenConfig[intersectKey].NameAddress)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.NameAddressRequestModels.Add(_mapper.Map<ScreeningInputTagModel, NameAddressRequestModel>(screeningInputTag));
                });
            }

            if (screenConfig[intersectKey].CountryAndCity)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.CountryCityRequestModels.Add(_mapper.Map<ScreeningInputTagModel, CountryCityRequestModel>(screeningInputTag));
                });
            }

            if (screenConfig[intersectKey].BicCode)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.BicRequestModels.Add(_mapper.Map<ScreeningInputTagModel, BicRequestModel>(screeningInputTag));
                });
            }

            if (screenConfig[intersectKey].Narrative)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.NarrativeRequestModels.Add(_mapper.Map<ScreeningInputTagModel, NarrativeRequestModel>(screeningInputTag));
                });
            }

            if (screenConfig[intersectKey].Port)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.PortRequestModels.Add(_mapper.Map<ScreeningInputTagModel, PortRequestModel>(screeningInputTag));
                });
            }

            if (screenConfig[intersectKey].Goods)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.GoodsRequestModels.Add(_mapper.Map<ScreeningInputTagModel, GoodsRequestModel>(screeningInputTag));
                });
            }
        }

        private void DispatchData(
            string intersectKey,
            Dictionary<string, List<ScreeningInputTagModel>> screeningInputTags,
            Dictionary<string, TfScreenSubConfigModel> screenSubConfigs,
            EdqRequestModel edqRequestModel)
        {
            if (screenSubConfigs[intersectKey].NameAddress)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.NameAddressRequestModels.Add(_mapper.Map<ScreeningInputTagModel, NameAddressRequestModel>(screeningInputTag));
                });
            }

            if (screenSubConfigs[intersectKey].CountryAndCity)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.CountryCityRequestModels.Add(_mapper.Map<ScreeningInputTagModel, CountryCityRequestModel>(screeningInputTag));
                });
            }

            if (screenSubConfigs[intersectKey].BicCode)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.BicRequestModels.Add(_mapper.Map<ScreeningInputTagModel, BicRequestModel>(screeningInputTag));
                });
            }

            if (screenSubConfigs[intersectKey].Narrative)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.NarrativeRequestModels.Add(_mapper.Map<ScreeningInputTagModel, NarrativeRequestModel>(screeningInputTag));
                });
            }

            if (screenSubConfigs[intersectKey].Port)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.PortRequestModels.Add(_mapper.Map<ScreeningInputTagModel, PortRequestModel>(screeningInputTag));
                });
            }

            if (screenSubConfigs[intersectKey].Goods)
            {
                screeningInputTags[intersectKey].ForEach(screeningInputTag =>
                {
                    edqRequestModel.GoodsRequestModels.Add(_mapper.Map<ScreeningInputTagModel, GoodsRequestModel>(screeningInputTag));
                });
            }
        }


    }
}