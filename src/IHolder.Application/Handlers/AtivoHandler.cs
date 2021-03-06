﻿using AutoMapper;
using IHolder.Application.Base;
using IHolder.Application.Commands;
using IHolder.Domain.DomainObjects;
using IHolder.Domain.Entities;
using IHolder.Domain.ValueObjects;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IHolder.Application.Handlers
{
    public class AtivoHandler : IRequestHandler<CadastrarAtivoCommand, bool>,
        IRequestHandler<AlterarAtivoCommand, bool>,
        IRequestHandler<AlterarSituacaoAtivoCommand, bool>
    {
        private readonly IRepositoryBase<Ativo> _repository;
        private readonly IRepositoryBase<DistribuicaoPorAtivo> _distribuicaoRepository;
        private readonly IHandlerBase _handlerBase;
        private readonly IMapper _mapper;

        public AtivoHandler(IRepositoryBase<Ativo> repository, IHandlerBase handlerBase, IMapper mapper, IRepositoryBase<DistribuicaoPorAtivo> distribuicaoRepository)
        {
            _repository = repository;
            _handlerBase = handlerBase;
            _mapper = mapper;
            _distribuicaoRepository = distribuicaoRepository;
        }

        public async Task<bool> Handle(CadastrarAtivoCommand request, CancellationToken cancellationToken)
        {
            if (TicketJaCadastrado(request.Ticker))
            {
                _handlerBase.PublishNotification("Já existe um ativo cadastrado com o mesmo Ticker");
                return false;
            }

            Ativo ativo = _mapper.Map<Ativo>(request);
            _repository.Insert(ativo);
            _distribuicaoRepository.Insert(new DistribuicaoPorAtivo(ativo.Id, request.UsuarioId, new Valores(0)));
            return await _repository.UnitOfWork.Commit();
        }

        private bool TicketJaCadastrado(string ticker, Nullable<Guid> Id = null)
        {
            return _repository.GetBy(a => a.Ticker == ticker && a.Id != Id).Result != null;
        }

        public async Task<bool> Handle(AlterarAtivoCommand request, CancellationToken cancellationToken)
        {
            if (TicketJaCadastrado(request.Ticker, request.Id))
            {
                _handlerBase.PublishNotification("Já existe um ativo cadastrado com o mesmo Ticker");
                return false;
            }

            _repository.Update(_mapper.Map<Ativo>(request));
            return await _repository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(AlterarSituacaoAtivoCommand request, CancellationToken cancellationToken)
        {
            Ativo ativo = await _repository.GetById(request.Id);
            ativo.AtualizarSituacao(request.Situacao);
            _repository.Update(ativo);
            return await _repository.UnitOfWork.Commit();
        }
    }
}
