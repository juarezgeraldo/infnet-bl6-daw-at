﻿using infnet_bl6_daw_at.Domain.Entities;
using infnet_bl6_daw_at.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AT.Models;
using AT.Data.Repositories;

namespace infnet_bl6_daw_at.Domain.Interfaces
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosService(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public async Task<IdentityResult> CreateUserAsync(Usuario usuario, string password)
        {
            return await _usuariosRepository.CreateUserAsync(usuario, password);
        }

        public async Task<bool> ValidateUserAsync(UsuarioLogin loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Token> CreateTokenAsync(UsuarioLogin usuario)
        {
            if (!await _usuariosRepository.ValidateUserAsync(usuario))
                return null;

            return await _usuariosRepository.CreateTokenAsync(usuario);
        }
    }
}
