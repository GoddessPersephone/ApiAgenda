﻿namespace AgendaContatoApi.DTO
{
    public class AlterarAgendaContatoDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string[]? Contato { get; set; }
        public string? Endereco { get; set; }
    }
}