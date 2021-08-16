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
        private readonly static EdqConfigModel _edqConfig = ConfigUtility.EdqConfigModel;
        private readonly MegaEcmUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly static string _baseUrl = _edqConfig.EdqBaseUrl;
        private readonly static string _nameAddressUrl = $"{_baseUrl}{_edqConfig.NameAddress}";
        private readonly static string _countryCityUrl = $"{_baseUrl}{_edqConfig.CountryCity}";
        private readonly static string _goodsUrl = $"{_baseUrl}{_edqConfig.Goods}";
        private readonly static string _narrativeUrl = $"{_baseUrl}{_edqConfig.Narrative}";
        private readonly static string _portUrl = $"{_baseUrl}{_edqConfig.Port}";
        private readonly static string _bicUrl = $"{_baseUrl}{_edqConfig.BIC}";

        private readonly Dictionary<string, TfScreenConfigModel> _screenConfigs;
        public EdqService()
        {
            _unitOfWork = new MegaEcmUnitOfWork();
            IEnumerable<TfScreenConfigModel> screenConfigs = _unitOfWork.ScreenConfigRepository.Query();
            _screenConfigs = screenConfigs.ToDictionary(c => c.TagName);


            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MxInputTagToRequestProfile());
                cfg.AddProfile(new EdqResponseToAlertsProfile());
            });

            _mapper = config.CreateMapper();

        }
        public async Task ProcessScreeningAsync(Dictionary<string, List<MxInputTagModel>> mxMessages)
        {
            var intersectKeys = mxMessages.Keys.Intersect(_screenConfigs.Keys);
            List<NameAddressRequestModel> nameAddressRequestModels = new List<NameAddressRequestModel>();
            List<CountryCityRequestModel> countryCitryRequestModels = new List<CountryCityRequestModel>();
            List<BicRequestModel> bicRequestModels = new List<BicRequestModel>();
            List<NarrativeRequestModel> narrativeRequestModels = new List<NarrativeRequestModel>();
            List<PortRequestModel> portRequestModels = new List<PortRequestModel>();
            List<GoodsRequestModel> goodsRequestModels = new List<GoodsRequestModel>();

            foreach (var intersectKey in intersectKeys)
            {

                if (_screenConfigs[intersectKey].NameAddress)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        nameAddressRequestModels.Add(_mapper.Map<MxInputTagModel, NameAddressRequestModel>(mxInputTag));
                    });
                }

                if (_screenConfigs[intersectKey].CountryAndCity)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        countryCitryRequestModels.Add(_mapper.Map<MxInputTagModel, CountryCityRequestModel>(mxInputTag));
                    });
                }

                if (_screenConfigs[intersectKey].BicCode)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        bicRequestModels.Add(_mapper.Map<MxInputTagModel, BicRequestModel>(mxInputTag));
                    });
                }

                if (_screenConfigs[intersectKey].Narrative)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        narrativeRequestModels.Add(_mapper.Map<MxInputTagModel, NarrativeRequestModel>(mxInputTag));
                    });
                }

                if (_screenConfigs[intersectKey].Port)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        portRequestModels.Add(_mapper.Map<MxInputTagModel, PortRequestModel>(mxInputTag));
                    });
                }


                if (_screenConfigs[intersectKey].Goods)
                {
                    mxMessages[intersectKey].ForEach(mxInputTag =>
                    {
                        goodsRequestModels.Add(_mapper.Map<MxInputTagModel, GoodsRequestModel>(mxInputTag));
                    });
                }

            }


            // Start Screen

            HttpClientSerivce service = new HttpClientSerivce();

            List<NameAddressResponseModel> nameAddressResponseModels = new List<NameAddressResponseModel>();
            List<CountryCityResponseModel> countryCitryResponseModels = new List<CountryCityResponseModel>();
            List<BicResponseModel> bicResponseModels = new List<BicResponseModel>();
            List<NarrativeResponseModel> narrativeResponseModels = new List<NarrativeResponseModel>();
            List<PortResponseModel> portResponseModels = new List<PortResponseModel>();
            List<GoodsResponseModel> goodsResponseModels = new List<GoodsResponseModel>();

            if (nameAddressRequestModels.Count() > 0)
            {
                nameAddressResponseModels = await service.PostAsync<List<NameAddressRequestModel>, List<NameAddressResponseModel>>(_nameAddressUrl, nameAddressRequestModels);
            }

            if (countryCitryRequestModels.Count() > 0)
            {
                countryCitryResponseModels = await service.PostAsync<List<CountryCityRequestModel>, List<CountryCityResponseModel>>(_countryCityUrl, countryCitryRequestModels);
            }

            if (bicRequestModels.Count() > 0)
            {
                bicResponseModels = await service.PostAsync<List<BicRequestModel>, List<BicResponseModel>>(_bicUrl, bicRequestModels);
            }

            if (narrativeRequestModels.Count() > 0)
            {
                narrativeResponseModels = await service.PostAsync<List<NarrativeRequestModel>, List<NarrativeResponseModel>>(_narrativeUrl, narrativeRequestModels);
            }

            if (portRequestModels.Count() > 0)
            {
                portResponseModels = await service.PostAsync<List<PortRequestModel>, List<PortResponseModel>>(_portUrl, portRequestModels);
            }

            if (goodsRequestModels.Count() > 0)
            {
                goodsResponseModels = await service.PostAsync<List<GoodsRequestModel>, List<GoodsResponseModel>>(_goodsUrl, goodsRequestModels);
            }

            var tfAlertsModels = new List<TfAlertsModel>();
            tfAlertsModels.AddRange(_mapper.Map<List<NameAddressResponseModel>, List<TfAlertsModel>>(nameAddressResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<CountryCityResponseModel>, List<TfAlertsModel>>(countryCitryResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<GoodsResponseModel>, List<TfAlertsModel>>(goodsResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<NarrativeResponseModel>, List<TfAlertsModel>>(narrativeResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<PortResponseModel>, List<TfAlertsModel>>(portResponseModels));
            tfAlertsModels.AddRange(_mapper.Map<List<GoodsResponseModel>, List<TfAlertsModel>>(goodsResponseModels));


            
            if (tfAlertsModels.Count() > 0)
            {
                TfCasesModel tfCasesModel = new TfCasesModel(MxPaser.oneMsg.id);
                tfCasesModel.CaseStatus = "New Case";
                tfCasesModel.CaseStatusCode = 0;
                tfAlertsModels.ForEach(c =>
                {
                    c.AlertStatus = "New Case";
                    c.AlertStatusCode = 0;
                    c.CaseId = tfCasesModel.Id;
                });

                try
                {
                    await _unitOfWork.TfMessagesRepository.InsertAsync(MxPaser.oneMsg);
                    await _unitOfWork.TfCasesRepository.InsertAsync(tfCasesModel);
                    await _unitOfWork.TfAlertsRepository.InsertAsync(tfAlertsModels);
                }
                catch (OracleException ex)
                {
                    _unitOfWork.Rollback();
                    Console.WriteLine(ex.Message, ex.ToString());
                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                }
                finally
                {
                    _unitOfWork.Commit();
                }

            }


        }
    }
}