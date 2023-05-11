using HtmlAgilityPack;

namespace CurrencyBot;

internal class WebScrapper
{
    public List<Currency> GetCurrencies()
    {
        var url = "https://www.nationalbank.kz/ru/exchangerates/ezhednevnye-oficialnye-rynochnye-kursy-valyut";
        var currencies = new List<Currency>();

        var web = new HtmlWeb();
        var doc = web.Load(url);

        var nodes = doc.DocumentNode.SelectNodes("//*[@id=\"s-page-content\"]/div/form/div/div[1]/table/tbody/tr[position()>1]");
        int counter = 0;

        foreach (var node in nodes)
        {
            var currency = new Currency()
            {
                CurrencyId = counter++,
                FullName = node.SelectSingleNode("td[2]").InnerText,
                ShortName = node.SelectSingleNode("td[3]").InnerText.Substring(0, 3),
                Value = double.Parse(node.SelectSingleNode("td[4]").InnerText),
            };

            currencies.Add(currency);
        }

        return currencies;
    }
}