using ProjectMVC.Models.Domain;
using System.Net;
using System.Xml;

namespace ProjectMVC.Data
{
    public class AddData
    {
        private readonly ForexDbContext _context;
        public AddData(ForexDbContext dbContext)
        {
            this._context = dbContext;
        }
        public void Add()
        {
            //  Forex forex = new Forex();
            try
            {
                //string tcmblink = string.Format("https://www.tcmb.gov.tr/kurlar/today.xml");


                // build XML request
                var requestXml = new XmlDocument();
                var responseXml = new XmlDocument();

                var httpRequest = HttpWebRequest.Create("https://www.tcmb.gov.tr/kurlar/today.xml");

                // set appropriate headers
                httpRequest.Method = "POST";
                httpRequest.ContentType = "text/xml";



                using (var requestStream = httpRequest.GetRequestStream())
                {
                    requestXml.Save(requestStream);
                }

                using (var response = (HttpWebResponse)httpRequest.GetResponse())
                using (var responseStream = response.GetResponseStream())
                {
                    // may want to check response.StatusCode to
                    // see if the request was successful

                    responseXml = new XmlDocument();
                    responseXml.Load(responseStream);
                }



                foreach (XmlNode node in responseXml.SelectNodes("Tarih_Date")[0].ChildNodes)
                {
                    var forex = new Forex()
                    {
                        Id = Guid.NewGuid(),
                        CurrencyCode = node.Attributes["CurrencyCode"].Value,
                        Unit = Convert.ToInt16(node["Unit"].InnerText),
                        CurrencyName = node["CurrencyName"].InnerText,
                        ForexBuying = Convert.ToDouble(node["ForexBuying"].InnerText),
                        ForexSelling = Convert.ToDouble(node["ForexSelling"].InnerText),
                        BanknoteBuying = Convert.ToDouble(node["BanknoteBuying"].InnerText),
                        BanknoteSelling = Convert.ToDouble(node["BanknoteSelling"].InnerText),
                        Date = DateTime.Now.Minute
                    };
                    List<Forex> forexList = _context.Forex.ToList();
                    foreach (Forex temp in forexList)
                        if (forex.CurrencyCode == temp.CurrencyCode)
                        {
                            temp.ForexBuying = forex.ForexBuying;
                            temp.ForexSelling = forex.ForexSelling;
                            temp.BanknoteBuying = forex.BanknoteBuying;
                            temp.BanknoteSelling = forex.BanknoteSelling;
                            temp.Date = DateTime.Now.Minute;
                            _context.SaveChanges();
                        }

                }

            }
            catch (Exception ex) { }
        }
    }
}

