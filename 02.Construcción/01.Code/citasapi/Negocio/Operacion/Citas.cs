using System;
using System.ComponentModel;
using CSharpFunctionalExtensions;
using Negocio.Catalogos; 
using Entity = Negocio.Core.Entity;

namespace Negocio.Operacion
{
    public class Citas : Entity
    {
        protected Citas()
        {

        }
        public static Result<Citas> Crear(
                                    Doctores doctor
                                   ,Usuarios usuario
                                   ,DateTime fechacita
                                   ,int horacita
                                   ,string paciente
                                   ,string correoelectronico 
                                )
        {
            DateTime combine = new DateTime(fechacita.Year, fechacita.Month, fechacita.Day, horacita, 0, 0);
            return new Citas
            {
                Doctor = doctor
                ,Usuario = usuario
                ,FechaCita = combine
                ,HoraCita = horacita
                ,Paciente = paciente
                ,CorreoElectronico = correoelectronico
                ,AuditDate = DateTime.UtcNow
                ,Estatus = EstatusCitas.Reservada
            };
        }
        #region Propiedades bd  
        public virtual Doctores Doctor { get; protected set; }
        public virtual Usuarios Usuario { get; protected set; }
        public virtual DateTime FechaCita { get; protected set; }
        public virtual int HoraCita { get; protected set; }
        public virtual string Paciente { get; protected set; }
        public virtual string CorreoElectronico { get; protected set; }
        public virtual EstatusCitas Estatus { get; set; }
        public virtual string AuditUser { get; protected set; } = "Anónimo";
        public virtual DateTime AuditDate { get; protected set; }

        #endregion
        #region Métodos   
        #endregion

        public enum EstatusCitas
        {
            [Description("Reservada")] Reservada = 1,
            [Description("Cancelada")] Cancelada = 2
        }
    }
     
}
