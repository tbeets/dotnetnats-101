// Copyright 2021 The NATS Authors
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using js_utils;
using NATS.Client;
using NATS.Client.JetStream;

namespace nats_js_subdds
{
    /// <summary>
    /// This example will demonstrate basic use of a pull subscription of:
    /// fetch pull: <c>Fetch(int batchSize, Duration or Millis maxWait)</c>
    /// </summary>
    internal static class JetStreamPullSubFetch
    {
        private const string Usage = 
            "Usage: nats-js-subdds [-url url] [-creds file] [-stream stream] " +
            "[-durable durable] [-count count]" +
            "\n\nDefault Values:" +
            "\n   [-stream]  foo" +
            "\n   [-durable] foocon1" +
            "\n   [-count]   15";

        public static void Main(string[] args)
        {
            ArgumentHelper helper = new ArgumentHelperBuilder("Pull Subscription using primitive Expires In", args, Usage)
                .DefaultStream("foo")
                .DefaultDurable("foocon1")
                .DefaultCount(15)
                .Build();

            try
            {
                using (IConnection c = new ConnectionFactory().CreateConnection(helper.MakeOptions()))
                {
                    // Create our JetStream context.
                    IJetStream js = c.CreateJetStreamContext();

                    PullSubscribeOptions pullOptions = PullSubscribeOptions.Builder()
                        .WithStream(helper.Stream)
                        .WithDurable(helper.Durable) // required
                        .WithBind(true)
                        .Build();

                    // subscribe
                    IJetStreamPullSubscription sub = js.PullSubscribe(helper.Subject, pullOptions);
                    c.Flush(1000);

                    int red = 0;
                    while (red < helper.Count)
                    {
                        IList<Msg> list = sub.Fetch(10, 1000);
                        foreach (Msg m in list)
                        {
                            Console.WriteLine($"{++red}. Message: {m}");
                            m.Ack();
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                helper.ReportException(ex);
            }
        }
    }
}
