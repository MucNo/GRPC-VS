using Grpc.Core;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;


namespace GrpcServer.Services
{
 
    public class PhoneService : Phone.PhoneBase
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;database=spam;username=root;password=NhaKhang0103");


        private readonly ILogger<PhoneService> _logger;

        public PhoneService(ILogger<PhoneService> logger)
        {
            logger = _logger;
        }


        public override Task<PhoneReply> GetPhoneNumber(PhoneRequest request, ServerCallContext context)
        {
            PhoneReply output = new PhoneReply();

            if (request.ID == 1)
            { 
            
            }
            //viet code lay data sql o

            return Task.FromResult(output);
        }


        public override async Task GetNewPhoneNumber(NewPhoneRequest request, IServerStreamWriter<PhoneReply> responseStream, ServerCallContext context)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }


                String insertQuery = @"Select * from phone limit 10";
                MySqlCommand command = new MySqlCommand(insertQuery, connection);

                MySqlDataReader reader = command.ExecuteReader();

                String PhoneNumbers = "";

                PhoneReply phoneReply = new PhoneReply();
                while (reader.Read())
                {
                    String phone = $"{reader.GetString("number")} ";
                    if (PhoneNumbers == "") PhoneNumbers = phone;
                    else PhoneNumbers += phone;

                }
                phoneReply.Number = PhoneNumbers;
                await responseStream.WriteAsync(phoneReply);

            }
            catch (Exception error)
            {
                String eror = error.Message;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

            }
        }
    }
}
