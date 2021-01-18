using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Application.Validators;
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
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        public async Task<BaseModel<UsuarioModel>> AtualizarAsync(UsuarioModel usuarioModel)
        {
            var usuario = _mapper.Map<Usuario>(usuarioModel);
            var result = _mapper.Map<UsuarioModel>(await _usuarioRepository.AtualizarAsync(usuario));

            return new BaseModel<UsuarioModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<UsuarioModel>> Autenticar(LoginModel loginModel)
        {
            var validator = await new LoginModelValidator().ValidateAsync(loginModel);
            if (!validator.IsValid)
            {
                return new BaseModel<UsuarioModel>(false, validator.Errors);
            }

            var result = await _usuarioRepository.AutenticarAsync(loginModel.Email, loginModel.Senha);

            if (result == default)
            {
                return new BaseModel<UsuarioModel>(false, EMensagens.EmailOuSenhaInvalidos);
            }

            var map = _mapper.Map<UsuarioModel>(result);
            map.Token = _tokenService.GenerateToken(map);

            var model = new BaseModel<UsuarioModel>(true, EMensagens.RealizadaComSucesso, map);
            return model;
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
