using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using GuestBookApi.Models.Responce;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace GuestBookApi.Repository.Messages
{
    public class MessagesRepository: IMessagesRepository
    {
        private IConfiguration _configuration;

        public MessagesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MessageListResponce GetMessages(PagingParameters pagingParameters)
        {
            MessageListResponce result = new MessageListResponce();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();

                DataTable dt = new DataTable();

                using (SqlCommand command = new SqlCommand("dbo.GetMessages", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;
                    // Параметры
                    command.Parameters.Add("@PageNumber", SqlDbType.Int).Value = pagingParameters.PageNumber;
                    command.Parameters.Add("@PageSize", SqlDbType.Int).Value = pagingParameters.PageSize;
                    command.Parameters.Add("@SortColumn", SqlDbType.NVarChar, 20).Value = pagingParameters.SortColumn;
                    command.Parameters.Add("@DescendingOrder", SqlDbType.Bit).Value = pagingParameters.DescendingOrder;
                    command.Parameters.Add("@RowsCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            result.MessageList.Add(new Message
                            {
                                DateC = dataReader.GetDateTime("DateC"),
                                UserName = dataReader.GetString("UserName"),
                                Email = dataReader.GetString("Email"),
                                Text = dataReader.GetString("Text")
                            });
                        }
                        dataReader.NextResult();
                        result.RowsCount = Convert.ToInt32(command.Parameters["@RowsCount"].Value);
                    }
                }
                connection.Close();
            }

            return result;
        }

        public void SaveMessage(SaveMessageParameters saveMessageParameters, string ip, string browser)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();

                DataTable dt = new DataTable();

                using (SqlCommand command = new SqlCommand("dbo.SaveMessage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;
                    // Параметры
                    command.Parameters.Add("@Ip", SqlDbType.NVarChar, 15).Value = ip;
                    command.Parameters.Add("@Browser", SqlDbType.NVarChar, 15).Value = browser;
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = saveMessageParameters.UserName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 20).Value = saveMessageParameters.Email;
                    command.Parameters.Add("@Homepage", SqlDbType.NVarChar, 2000).Value = saveMessageParameters.Homepage;
                    command.Parameters.Add("@Text", SqlDbType.NVarChar).Value = WebUtility.HtmlEncode(saveMessageParameters.Text);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
