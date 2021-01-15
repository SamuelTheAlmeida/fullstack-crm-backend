using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Repositories;
using FullStackCRM.Shared;
using System;
using System.Collections.Generic;
using System.Text;

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

        public UsuarioModel Autenticar(string usuario, string senha)
        {
            var result = _mapper.Map<UsuarioModel>(_usuarioRepository.AutenticarAsync(usuario, senha));
            return result;
        }
    }
}
