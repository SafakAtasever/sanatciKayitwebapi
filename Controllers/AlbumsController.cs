using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SanatciKayit.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SanatciKayit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Databases\\Artist.mdf;Integrated Security=True;Connect Timeout=30";
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(ILogger<AlbumController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAlbum([FromBody] Album album)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("CreateAlbum", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ArtistId", album.ArtistId);
                cmd.Parameters.AddWithValue("@Name", album.Name);
                cmd.Parameters.AddWithValue("@ReleaseDate", album.ReleaseDate);
                cmd.Parameters.AddWithValue("@Duration", album.DurationInSeconds);
                cmd.Parameters.AddWithValue("@SalesCount", album.SalesCount);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }

            return Ok("Album created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlbum(int id, [FromBody] Album album)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateAlbum", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", album.Name);
                cmd.Parameters.AddWithValue("@ReleaseDate", album.ReleaseDate);
                cmd.Parameters.AddWithValue("@Duration", album.DurationInSeconds);
                cmd.Parameters.AddWithValue("@SalesCount", album.SalesCount);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }

            return Ok("Album updated successfully");
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbum()
        {
            List<Album> albums = new List<Album>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllAlbum", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    albums.Add(new Album
                    {
                        Id = (int)reader["Id"],
                        ArtistId = (int)reader["ArtistId"],
                        Name = reader["Name"].ToString(),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        DurationInSeconds = (int)reader["Duration"],
                        SalesCount = (int)reader["SalesCount"],
                        CreatedDate = (DateTime)reader["CreatedDate"],
                        UpdatedDate = (DateTime)reader["UpdatedDate"]
                    });
                }

                conn.Close();
            }

            return Ok(albums);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteAlbum", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }

            return Ok();
        }
    }
}
