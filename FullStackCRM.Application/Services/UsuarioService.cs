using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
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

        public async Task<BaseModel<UsuarioModel>> AtualizarAsync(UsuarioModel usuarioModel)
        {
            var usuario = _mapper.Map<Usuario>(usuarioModel);
            var result = _mapper.Map<UsuarioModel>(await _usuarioRepository.AtualizarAsync(usuario));
            return new BaseModel<UsuarioModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<UsuarioModel> Autenticar(LoginModel loginModel)
        {
            try
            {
                var result = await _usuarioRepository.AutenticarAsync(loginModel.Email, loginModel.Senha);
                var model = _mapper.Map<UsuarioModel>(result);
                return model;
            } catch (Exception e )
            {
                throw e;
            }

        }

        public async Task<BaseModel<UsuarioModel>> InserirAsync(UsuarioModel usuarioModel)
        {
            var usuario = _mapper.Map<Usuario>(usuarioModel);
            var result = _mapper.Map<UsuarioModel>(await _usuarioRepository.InserirAsync(usuario));
            return new BaseModel<UsuarioModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<List<UsuarioModel>>> ListarAsync()
        {
            var result = _mapper.Map<List<UsuarioModel>>(await _usuarioRepository.ListarAsync());
            return new BaseModel<List<UsuarioModel>>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<UsuarioModel>> ObterPorIdAsync(Guid id)
        {
            var result = _mapper.Map<UsuarioModel>(await _usuarioRepository.ObterPorIdAsync(id));
            return new BaseModel<UsuarioModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<UsuarioModel>> ExcluirAsync(Guid id)
        {
            await _usuarioRepository.ExcluirAsync(id);
            return new BaseModel<UsuarioModel>(true, EMensagens.RealizadaComSucesso);
        }
    }
}
