using CommonMegaAp11.Enums;
using System;

namespace MegaTFLT.Services.Parsers
{
    public static class PaserFactory
    {
        public static BaseMessageParser PaserType(MessageSource messageSource)
        {
            switch (messageSource)
            {
                case MessageSource.Mx:
                    return  new MxParser();
                case MessageSource.TxnObs:
                    return new TxnParser();
                default:
                    throw new NotImplementedException();
            }             
        }
    }
}