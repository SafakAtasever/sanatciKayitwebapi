using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SanatciKayit.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SanatciKayit.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Databases\\Artist.mdf;Integrated Security=True;Connect Timeout=30";
        private readonly ILogger<ArtistController> _logger;

        public ArtistController(ILogger<ArtistController> logger)
        {
            _logger = logger;
        }

        // Create Artist
        [HttpPost]
        public async Task<IActionResult> CreateArtist([FromBody] Artist artist)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("CreateArtist", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FirstName", artist.FirstName);
                cmd.Parameters.AddWithValue("@LastName", artist.LastName);
                cmd.Parameters.AddWithValue("@BirthDate", artist.BirthDate);
                cmd.Parameters.AddWithValue("@Email", artist.Email);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }

            return Ok();
        }

        // Update Artist
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, [FromBody] Artist artist)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateArtist", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@FirstName", artist.FirstName);
                cmd.Parameters.AddWithValue("@LastName", artist.LastName);
                cmd.Parameters.AddWithValue("@BirthDate", artist.BirthDate);
                cmd.Parameters.AddWithValue("@Email", artist.Email);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }

            return Ok();
        }

        // Get all Artists
        [HttpGet]
        public async Task<IActionResult> GetArtists()
        {
            List<Artist> artists = new List<Artist>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllArtists", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    artists.Add(new Artist
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        BirthDate = (DateTime)reader["BirthDate"],
                        Email = reader["Email"].ToString(),
                        CreatedDate = (DateTime)reader["CreatedDate"],
                        UpdatedDate = (DateTime)reader["UpdatedDate"]
                    });
                }

                conn.Close();
            }

            return Ok(artists);
        }

        // Delete Artist
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteArtist", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                await cmd.ExecuteNonQueryAsync();
                conn.Close();
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistDetails(int id)
        {
            List<ArtistDetail> artists = new List<ArtistDetail>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetArtistDetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ArtistId", id);

                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (reader.Read())
                {
                    artists.Add(new ArtistDetail
                    {
                        Id = (int)reader["Id"],
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        BirthDate = (DateTime)reader["BirthDate"],
                        Email = reader["Email"].ToString(),
                        CreatedDate = (DateTime)reader["CreatedDate"],
                        UpdatedDate = (DateTime)reader["UpdatedDate"],
                        TotalAlbums = (int)reader["TotalAlbums"],
                        TotalDurationInSeconds = (int)reader["TotalDurationInSeconds"],
                        TotalSalesCount = (int)reader["TotalSalesCount"],
                        TotalHours = (int)reader["TotalHours"],
                        TotalMinutes = (int)reader["TotalMinutes"]


                    });
                }

                conn.Close();
            }

            return Ok(artists);
        }
    }
}
