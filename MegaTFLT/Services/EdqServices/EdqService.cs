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
                cfg.AddProfile(new MxInputTagToRequestProfile());
                cfg.AddProfile(new EdqResponseToAlertsProfile());
            });

            _mapper = config.CreateMapper();

        }
        public async Task<List<TfAlertsModel>> ProcessScreeningAsync(Dictionary<TfScreenConfigKeyModel, List<ScreeningInputTagModel>> mxMessages)
        {
            var intersectKeys = mxMessages.Keys.Intersect(ConfigUtility.ScreenConfigs.Keys);

            EdqRequestModel edqRequestModel = new EdqRequestModel();

            this.DispatchScreenData(mxMessages, edqRequestModel);

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

        private void DispatchScreenData(Dictionary<TfScreenConfigKeyModel, List<ScreeningInputTagModel>> mxMessages, EdqRequestModel edqRequestModel)
        {
            var intersectKeys = mxMessages.Keys.Intersect(ConfigUtility.ScreenConfigs.Keys);
            foreach (TfScreenConfigKeyModel intersectKey in intersectKeys)
            {

                if (ConfigUtility.ScreenConfigs[intersectKey].NameAddress)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        edqRequestModel.NameAddressRequestModels.Add(_mapper.Map<ScreeningInputTagModel, NameAddressRequestModel>(mxInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].CountryAndCity)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        edqRequestModel.CountryCityRequestModels.Add(_mapper.Map<ScreeningInputTagModel, CountryCityRequestModel>(mxInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].BicCode)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        edqRequestModel.BicRequestModels.Add(_mapper.Map<ScreeningInputTagModel, BicRequestModel>(mxInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].Narrative)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        edqRequestModel.NarrativeRequestModels.Add(_mapper.Map<ScreeningInputTagModel, NarrativeRequestModel>(mxInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].Port)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        edqRequestModel.PortRequestModels.Add(_mapper.Map<ScreeningInputTagModel, PortRequestModel>(mxInputTag));
                    });
                }

                if (ConfigUtility.ScreenConfigs[intersectKey].Goods)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        edqRequestModel.GoodsRequestModels.Add(_mapper.Map<ScreeningInputTagModel, GoodsRequestModel>(mxInputTag));
                    });
                }

            }
        }
    }
}