﻿// Copyright (c) 2013 Richard Long & HexBeerium
//
// Released under the MIT license ( http://opensource.org/licenses/MIT )
//

using System;
using System.Collections.Generic;
using System.Text;
using dotnet.lib.Http.json_broker;
using dotnet.lib.CoreAnnex.exception;

namespace dotnet.lib.Http.json_broker.server
{
    public interface JavascriptCallbackAdapter
    {

        void onFault(BrokerMessage request, Exception fault);
        void onResponse(BrokerMessage request, BrokerMessage response);

    }
}
