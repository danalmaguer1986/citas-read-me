using CSharpFunctionalExtensions;
using Negocio.Catalogos;
using Negocio.Core;
using Negocio.DataAccess;
using Negocio.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Operacion
{
    public class CitasNuevaCommand : ICommand
    {
        public int DoctorId { get; set; }
        public DateTime FechaCita { get; set; }
        public int HoraCita { get; set; }
        public string NombrePaciente { get; set; }
        public string CorreoElectronico { get; set; }
        public int UsuarioId { get; set; }

        internal class CitasNuevaCommandHandler : ICommandHandler<CitasNuevaCommand>
        {
            private readonly IReadOnlyRepository _readOnlyRepository;
            private readonly IRepository<Citas> _citasRepository;

            public CitasNuevaCommandHandler(IReadOnlyRepository readOnlyRepository
                                            , IRepository<Citas> citasRepository)
            {
                this._readOnlyRepository = readOnlyRepository;
                this._citasRepository = citasRepository;
            }

            public Result Handle(CitasNuevaCommand command)
            {
                var doctorOrError = _readOnlyRepository.Get<Doctores>(command.DoctorId);
                if(doctorOrError.IsFailure)
                {
                    return Result.Failure(Errors.Catalogos_Doctores.NotFound);
                }
                var usuario = _readOnlyRepository.GetOrDefault<Usuarios>(command.UsuarioId);

                var citaOrError = Citas.Crear
                                (doctorOrError.Value
                                , usuario
                                , command.FechaCita
                                , command.HoraCita
                                , command.NombrePaciente
                                , command.CorreoElectronico);

                if(citaOrError.IsFailure)
                {
                    return Result.Failure(Errors.Citas.General);
                }
                _citasRepository.Save(citaOrError.Value);
                return Result.Success();
            }
        }

    }
}
