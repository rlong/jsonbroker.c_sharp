﻿// Copyright (c) 2013 Richard Long & HexBeerium
//
// Released under the MIT license ( http://opensource.org/licenses/MIT )
//

using System;
using System.Collections.Generic;
using System.Text;
using jsonbroker.library.common.http;
using jsonbroker.library.common.log;

namespace jsonbroker.library.server.http.reqest_handler
{
    public class OpenRequestHandler : RequestHandler
    {


        private static Log log = Log.getLog(typeof(OpenRequestHandler));

        public static readonly String REQUEST_URI = "/_dynamic_/open";

        private Dictionary<String, RequestHandler> _processors;

        public OpenRequestHandler()
        {
            _processors = new Dictionary<String, RequestHandler>();
        }


        public void addHttpProcessor(RequestHandler processor)
        {
            String requestUri = REQUEST_URI + processor.getProcessorUri();
            log.debug(requestUri, "requestUri");
            _processors[requestUri] = processor;
        }


        private RequestHandler getHttpProcessor(String requestUri)
        {
            log.debug(requestUri, "requestUri");

            int indexOfQuestionMark = requestUri.IndexOf('?');
            if (-1 != indexOfQuestionMark)
            {
                requestUri = requestUri.Substring(0, indexOfQuestionMark);
            }

            if (!_processors.ContainsKey(requestUri))
            {
                return null;
            }

            RequestHandler answer = _processors[requestUri];
            return answer;

        }


        public String getProcessorUri()
        {
            return REQUEST_URI;
        }


        public HttpResponse processRequest(HttpRequest request)
        {

            String requestUri = request.RequestUri;
            RequestHandler httpProcessor = getHttpProcessor(requestUri);


            if (null == httpProcessor)
            {
                log.errorFormat("null == httpProcessor; requestUri = {0}", requestUri);
                throw HttpErrorHelper.notFound404FromOriginator(this);
            }

            return httpProcessor.processRequest(request);

        }

    }
}