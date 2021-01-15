using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Repositories;
using FullStackCRM.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioModel> Autenticar(string usuario, string senha)
        {
            try
            {
                var result = await _usuarioRepository.AutenticarAsync(usuario, senha);
                var model = _mapper.Map<UsuarioModel>(result);
                return model;
            } catch (Exception e )
            {
                throw e;
            }

        }
    }
}
