using GuestBookApi.Models;
using GuestBookApi.Models.MessageList;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public IEnumerable<MessageView> GetMessages(PagingParameters pagingParameters, out int rowsCount)
        {
            List<MessageView> result = new List<MessageView>();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();

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

                            result.Add(new MessageView
                            {
                                DateC = dataReader.GetDateTime("DateC"),
                                UserName = dataReader.GetString("UserName"),
                                Email = dataReader.GetString("Email"),
                                Text = dataReader.GetString("Text")
                            });
                        }
                        dataReader.NextResult();
                        rowsCount = Convert.ToInt32(command.Parameters["@RowsCount"].Value ?? 0);
                    }
                }
                connection.Close();
            }

            return result;
        }

        public void SaveMessage(Message message)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DBConnection")))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.SaveMessage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 360;
                    // Параметры
                    command.Parameters.Add("@Ip", SqlDbType.NVarChar, 15).Value = message.Ip ?? "";
                    command.Parameters.Add("@Browser", SqlDbType.NVarChar, 15).Value = message.Browser ?? "";
                    command.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = message.UserName;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 20).Value = message.Email;
                    command.Parameters.Add("@Homepage", SqlDbType.NVarChar, 2000).Value = WebUtility.HtmlEncode(message.Homepage);
                    command.Parameters.Add("@Text", SqlDbType.NVarChar).Value = WebUtility.HtmlEncode(message.Text);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
