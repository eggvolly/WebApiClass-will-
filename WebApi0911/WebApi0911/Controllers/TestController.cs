using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi0911.Controllers
{
    public class TestController : ApiController
    {
        public class GeoPoint
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        /// <summary>
        /// 使用網址接資料 (基本上Get都是利用網址取得資訊
        /// http://localhost:15557/sample1?name=Rat&age=18
        /// </summary>
        /// <param name="name">取得網址上的name資訊</param>
        /// <param name="age">取得網址上的age資訊</param>
        /// <returns></returns>
        [Route("sample1")]
        public IHttpActionResult Get1(string name, string age)
        {
            return Ok(name+"-"+age);
        }

        /// <summary>
        /// 使用POST搭配FromBody使用
        /// FromBody會強制到body取值，若使用FromBody會將body的資訊完全儲存到對應的參數
        /// 因此若帶有其他額外資訊在body並不會自動去做解析→只能放age的內容
        /// http://localhost:15557/sample2?name=Ray
        /// body content : 18
        /// </summary>
        /// <param name="name">從網址列取的name的資訊</param>
        /// <param name="age">將Body的內容放到age裡</param>
        /// <returns></returns>
        [Route("sample2")]
        public IHttpActionResult PostSample2(string name, [FromBody]string age)
        {
            return Ok(name + "-" + age);
        }

        /// <summary>
        /// 使用POST搭配強型別強迫到網址列取值
        /// http://localhost:15557/sample3?Latutide=123&Longitude=aaa
        /// </summary>
        /// <param name="point">將網址列的資訊轉成強型別資訊傳入function</param>
        /// <returns></returns>
        [Route("Sample3")]
        public IHttpActionResult PostSample3([FromUri]GeoPoint point)
        {
            return Ok(point);
        }
    }
}
