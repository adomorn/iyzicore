using System;
using Iyzicore.Request;

namespace Iyzicore.Model.V2.Iyzilink
{
    public class IyziLink : IyzipayResourceV2
    {
        private static string V2_IYZILINK_PRODUCTS = "/v2/iyzilink/products";
        
        public static ResponseData<IyziLinkSave> Create(IyziLinkSaveRequest request, Options options)
        {
            var uri = options.BaseUrl + V2_IYZILINK_PRODUCTS + GetQueryParams(request);
            return RestHttpClientV2.Create().Post<ResponseData<IyziLinkSave>>(uri, GetHttpHeadersWithRequestBody(request, uri ,options), request);
        }
        
        public static ResponseData<IyziLinkSave> Update(string token, IyziLinkSaveRequest request, Options options)
        {
            var uri = options.BaseUrl + V2_IYZILINK_PRODUCTS + "/" + token + GetQueryParams(request);
            return RestHttpClientV2.Create().Put<ResponseData<IyziLinkSave>>(uri, GetHttpHeadersWithRequestBody(request, uri ,options), request);
        }
        
        public static ResponsePagingData<IyziLinkItem> RetrieveAll(PagingRequest request, Options options)
        {
            var queryParams = GetQueryParams(request);
            var iyzilinkQueryParam = "productType=IYZILINK";
            
            queryParams = string.IsNullOrEmpty(queryParams)
                ? "?" + iyzilinkQueryParam
                : queryParams + "&" + iyzilinkQueryParam;
            
            var uri = options.BaseUrl + V2_IYZILINK_PRODUCTS + queryParams;
            return RestHttpClientV2.Create().Get<ResponsePagingData<IyziLinkItem>>(uri, GetHttpHeadersWithUrlParams(request,uri ,options));
        }
        
        public static ResponseData<IyziLinkItem> Retrieve(string token, BaseRequestV2 request, Options options)
        {
            var uri = options.BaseUrl + V2_IYZILINK_PRODUCTS + "/" + token + GetQueryParams(request);
            return RestHttpClientV2.Create().Get<ResponseData<IyziLinkItem>>(uri, GetHttpHeadersWithUrlParams(request, uri ,options));
        }
        
        public static IyzipayResourceV2 Delete(string token, BaseRequestV2 request, Options options)
        {
            var uri = options.BaseUrl + V2_IYZILINK_PRODUCTS + "/" + token + GetQueryParams(request);
            return RestHttpClientV2.Create().Delete<IyzipayResourceV2>(uri, GetHttpHeadersWithRequestBody(request, uri ,options),request);
        }
        
        private static string GetQueryParams(BaseRequestV2 request) {
            if (request == null) {
                return "";
            }

            var queryParams = "?conversationId=" + request.ConversationId;

            if (!string.IsNullOrEmpty(request.Locale)) {
                queryParams += "&locale=" + request.Locale;
            }

            if (!(request is PagingRequest pagingRequest)) return queryParams;
            
            if (pagingRequest.Page != null) {
                queryParams += "&page=" + pagingRequest.Page;
            }

            if (pagingRequest.Count != null) {
                queryParams += "&count=" + pagingRequest.Count;
            }
            return queryParams;
        }
    }
}