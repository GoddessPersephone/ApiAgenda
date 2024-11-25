﻿using AgendaContatoApi.Model;

namespace AgendaContatoApi.Interface
{
    public interface IAgendaContatoService
    {
        Task<List<ContatoModel>> Obter();
        Task<ContatoModel> ObterPorId(int id);
        Task<List<ContatoModel>> Inserir(List<ContatoModel> liContato);
        Task<ContatoModel> Alterar(ContatoModel contato);
        Task<ContatoModel> Deletar(int id);
    }
}