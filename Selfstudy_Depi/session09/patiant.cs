using System;
using System.Collections.Generic;
using System.Text;

namespace patiant
{
    // Q1: كلاس Patient بـ Primary Constructor (C# 12)
// البارامترات في الـ primary constructor مش properties لوحدها
// فبنستخدمها عشان نعمل initialize للـ properties
public class Patient(int id, string fullName, string phoneNumber, string medicalHistory)
{
    public int Id { get; } = id;
    public string FullName { get; } = fullName;
    public string PhoneNumber { get; } = phoneNumber;
    public string MedicalHistory { get; } = medicalHistory;

    public override string ToString()
    {
        return $"Id: {Id} :: FullName: {FullName} :: PhoneNumber: {PhoneNumber} :: MedicalHistory: {MedicalHistory}";
    }
}

// Q3: Positional record بيعرض Id و FullName و PhoneNumber بس (من غير MedicalHistory)
public record PatientDto(int Id, string FullName, string PhoneNumber);

// Q4: Mapper بيحوّل Patient لـ PatientDto
public static class PatientMapper
{
    public static PatientDto MapFromModelToDto(Patient patient)
    {
        return new PatientDto(patient.Id, patient.FullName, patient.PhoneNumber);
    }
}
}

