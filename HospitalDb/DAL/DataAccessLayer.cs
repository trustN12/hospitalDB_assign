using System.Data;
using HospitalDb.Models;
using Microsoft.Data.SqlClient;

namespace HospitalDb.DAL;

public class DataAccessLayer
{
    private readonly string conString;
    private readonly SqlConnection _con;

    public DataAccessLayer(IConfiguration config)
    {
        conString = config.GetConnectionString("ProjectConfig");
        _con = new SqlConnection(conString);
    }

    public List<Patient> GetPatients()
    {
        List<Patient> list = new List<Patient>();

        SqlCommand cmd = new SqlCommand("spPatient_GetAll", _con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {
            Patient p = new Patient();

            p.Pid = Convert.ToInt32(dr["Pid"]);
            p.Fname = dr["Fname"].ToString();
            p.Lname = dr["Lname"].ToString();
            p.Gender = dr["Gender"].ToString();
            p.Did = Convert.ToInt32(dr["Did"]);
            p.DoctorName = dr["DoctorName"].ToString();
            p.Specialisation = dr["Specialisation"].ToString();

            list.Add(p);
        }

        return list;
    }

    public List<Doctor> GetDoctors()
    {
        List<Doctor> list = new List<Doctor>();

        SqlCommand cmd = new SqlCommand("spDoctor_GetAll", _con);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {
            Doctor d = new Doctor();

            d.Did = Convert.ToInt32(dr["Did"]);
            d.Name = dr["Name"].ToString();
            d.Specialisation = dr["Specialisation"].ToString();  // ⭐ ADD THIS LINE

            list.Add(d);
        }

        return list;
    }

    public void InsertPatient(Patient p)
    {
        SqlCommand cmd = new SqlCommand("spPatient_Insert", _con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Fname", p.Fname);
        cmd.Parameters.AddWithValue("@Lname", p.Lname);
        cmd.Parameters.AddWithValue("@Gender", p.Gender);
        cmd.Parameters.AddWithValue("@Did", p.Did);

        _con.Open();
        cmd.ExecuteNonQuery();
        _con.Close();
    }

    public void UpdatePatient(Patient p)
    {
        SqlCommand cmd = new SqlCommand("spPatient_Update", _con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Pid", p.Pid);
        cmd.Parameters.AddWithValue("@Fname", p.Fname);
        cmd.Parameters.AddWithValue("@Lname", p.Lname);
        cmd.Parameters.AddWithValue("@Gender", p.Gender);
        cmd.Parameters.AddWithValue("@Did", p.Did);

        _con.Open();
        cmd.ExecuteNonQuery();
        _con.Close();
    }

    public void DeletePatient(int id)
    {
        SqlCommand cmd = new SqlCommand("spPatient_Delete", _con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Pid", id);

        _con.Open();
        cmd.ExecuteNonQuery();
        _con.Close();
    }
    
    
    
    public Patient GetPatientById(int id)
    {
        Patient p = new Patient();

        SqlCommand cmd = new SqlCommand("spPatient_GetById", _con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@Pid", id);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];

            p.Pid = Convert.ToInt32(dr["Pid"]);
            p.Fname = dr["Fname"].ToString();
            p.Lname = dr["Lname"].ToString();
            p.Gender = dr["Gender"].ToString();

            p.Did = Convert.ToInt32(dr["Did"]);   // 🔥🔥🔥 THIS LINE FIXES EVERYTHING

            p.DoctorName = dr["DoctorName"].ToString();
            p.Specialisation = dr["Specialisation"].ToString();
        }

        return p;
    }
    
    
    public List<Patient> SearchPatients(string searchText)
    {
        List<Patient> list = new List<Patient>();

        SqlCommand cmd = new SqlCommand("spPatient_Search", _con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@SearchText", searchText);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        foreach (DataRow dr in dt.Rows)
        {
            Patient p = new Patient();

            p.Pid = Convert.ToInt32(dr["Pid"]);
            p.Fname = dr["Fname"].ToString();
            p.Lname = dr["Lname"].ToString();
            p.Gender = dr["Gender"].ToString();
            p.DoctorName = dr["DoctorName"].ToString();
            p.Specialisation = dr["Specialisation"].ToString();

            list.Add(p);
        }

        return list;
    }
}