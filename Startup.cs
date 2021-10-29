using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChainApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    string id = "leoputti@hotmail";
                    CBlockChain myBlockChain = new CBlockChain(id);
                    myBlockChain.AddGenesisBlock();

                    var trans = new List<CTransaction>();
                    trans.Add(new CTransaction("A", "B", "Hello,I'd like to deposit 10USD"));
                    trans.Add(new CTransaction("B", "A", "10USD DEPOSITED"));
                    myBlockChain.AddBlock(new CBlock(id,DateTime.Now,trans));
                    
                    trans = new List<CTransaction>();
                    trans.Add(new CTransaction("C", "B", "I'd like to transfer my account for 50USD"));
                    trans.Add(new CTransaction("B", "C", "DO YOU WANT TO TRANSFER TO WHOM?"));
                    trans.Add(new CTransaction("C", "B", "Transfer To A"));
                    trans.Add(new CTransaction("B", "C", "50USD Transfered"));
                    myBlockChain.AddBlock(new CBlock(id, DateTime.Now, trans));

                    trans = new List<CTransaction>();
                    trans.Add(new CTransaction("A", "B", "Hello,I'd like to know my balance now"));
                    trans.Add(new CTransaction("B", "A", "60USD TOTAL"));
                    myBlockChain.AddBlock(new CBlock(id, DateTime.Now, trans));
                    string strResult = "RAW DATA\n";
                    foreach(CBlock block in myBlockChain.Chain)
                    {
                        if (block.Transactions != null)
                        {
                            foreach (CTransaction tran in block.Transactions)
                            {
                                strResult += "FROM:" + tran.From + " ";
                                strResult += "TO:" + tran.To + " ";
                                strResult += "DATA:"+ tran.Data.ToString() + "\n";
                            }

                        }
                    }
                    strResult += "------------------------------\n";
                    strResult += "Total Chains=" + myBlockChain.Chain.Count + "\n";
                    for(int i=1;i<myBlockChain.Chain.Count;i++)
                    {
                        var data = myBlockChain.GetBlock(i);
                        strResult += "---------------Chain #"+data.Index+"---------------\n";
                        strResult += "PREVIOUS HASH:" + data.PreviousHash + "\n";
                        foreach (CTransaction tran in data.Transactions)
                        {
                            strResult += "- FROM:" +  tran.From + " ";
                            strResult += "- TO:" + tran.To + " >> ";
                            strResult += tran.Data +"\n";
                        }
                        strResult += "NEXT HASH:" + data.Hash + "\n";
                    }
                    await context.Response.WriteAsync(strResult);
                });
            });
        }
    }
}
