using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using CorporateserveTranslateDLL;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class webservice : System.Web.Services.WebService {

    public webservice () {

       
    }

    [WebMethod]
    public string TranslateEng2Hindi(String InputText) {
        LanguageTranslator LT = new LanguageTranslator();
        string ConvertedText = LT.Translate(InputText.ToString().ToLower(), "English", "Hindi");
        return ConvertedText;
    }
    
}
