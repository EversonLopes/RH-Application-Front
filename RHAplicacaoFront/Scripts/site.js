/*Configuração do jQuery DataTable
    -   Ordenação pela coluna 2, que é referente ao percentual.
 */

$(document).ready(function () {
    $('.table').DataTable({
        "language": {
            "lengthMenu": "Exibir  _MENU_  Registros por página",
            "search": "Procurar ",
            "paginate": { "previous": "Anterior", "next": "Próximo" },
            "zeroRecords": "Nenhum registro encontrado",
            "info": "Exibindo página _PAGE_ de _PAGES_",
            "infoEmpty": "Sem registros",
            "infoFiltered": "(filtrado de _MAX_ regitros totais)"
        },
        "processing": true,    // mostrar a progress bar
        "filter": true,            // habilita o filtro(search box)
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]], //define as opções de paginação
        "pageLength": 10,      // define o tamanho da página 
    });

    $('.relatorio').DataTable({
        "language": {
            "lengthMenu": "Exibir  _MENU_  Registros por página",
            "search": "Procurar ",
            "paginate": { "previous": "Anterior", "next": "Próximo" },
            "zeroRecords": "Nenhum registro encontrado",
            "info": "Exibindo página _PAGE_ de _PAGES_",
            "infoEmpty": "Sem registros",
            "infoFiltered": "(filtrado de _MAX_ regitros totais)"
        },
        "processing": true,    // mostrar a progress bar
        "filter": true,            // habilita o filtro(search box)
        "lengthMenu": [[5, 10, 25, 50, -1], [5, 10, 25, 50, "Todos"]], //define as opções de paginação
        "pageLength": 10,      // define o tamanho da página 
        "order": [[2, 'desc']]

    });

});



