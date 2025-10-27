Funcionalidade: Listagem das entidades via API
  Como usuário do sistema ESG
  Quero listar entidades pelas APIs
  Para visualizar os dados com respostas consistentes

  Contexto:
    Dado que o sistema está em execução

  
  # ENDPOINTS QUE RETORNAM ARRAY
  
  Esquema do Cenário: [ARRAY] Retornar 200 e lista com itens
    Quando eu solicito GET para "<rota>"
    Então o status deve ser 200
    E o corpo deve ser um JSON array com pelo menos 1 item
    E o contrato deve obedecer ao schema básico de array de objetos

    Exemplos:
      | rota               |
      | /api/compensacoes |
      | /api/empresas     |
      | /api/historicos   |
      | /api/relatorios   |

  Esquema do Cenário: [ARRAY] Retornar 200 e lista vazia quando não houver dados
    Quando eu solicito GET para "<rota>" com base vazia
    Então o status deve ser 200
    E o corpo deve ser um JSON array vazio
    E o contrato deve obedecer ao schema básico de array de objetos

    Exemplos:
      | rota               |
      | /api/compensacoes |
      | /api/empresas     |
      | /api/historicos   |
      | /api/relatorios   |

  Esquema do Cenário: [ARRAY] Retornar 500 em caso de exceção interna
    Quando eu solicito GET para "<rota>" e ocorre um erro inesperado
    Então o status deve ser 500

    Exemplos:
      | rota               |
      | /api/compensacoes |
      | /api/empresas     |
      | /api/historicos   |
      | /api/relatorios   |

  
  # ENDPOINTS QUE RETORNAM PAGINADO (PagedResult)
 
  Esquema do Cenário: [PAGED] Retornar 200 e itens em PagedResult
    Quando eu solicito GET para "<rota>" com "page=1" e "pageSize=10"
    Então o status deve ser 200
    E o corpo deve ser um JSON objeto com os campos "page", "pageSize", "totalItems" e "items"
    E "items" deve conter pelo menos 1 item
    E o contrato deve obedecer ao schema mínimo de PagedResult

    Exemplos:
      | rota          |
      | /api/usuarios |

  Esquema do Cenário: [PAGED] Retornar 200 e items vazio em PagedResult
    Quando eu solicito GET para "<rota>" com "page=1" e "pageSize=10" com base vazia
    Então o status deve ser 200
    E o corpo deve ser um JSON objeto com os campos "page", "pageSize", "totalItems" e "items"
    E "items" deve estar vazio
    E o contrato deve obedecer ao schema mínimo de PagedResult

    Exemplos:
      | rota          |
      | /api/usuarios |

  Esquema do Cenário: [PAGED] Retornar 500 em caso de exceção interna
    Quando eu solicito GET para "<rota>" com "page=1" e "pageSize=10" e ocorre um erro inesperado
    Então o status deve ser 500

    Exemplos:
      | rota          |
      | /api/usuarios |
