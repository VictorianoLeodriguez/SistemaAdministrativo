/**
 * SistemaAdm — Sidebar JS (jQuery)
 * Responsável por:
 *   1. Abrir / fechar no mobile (hambúrguer)
 *   2. Expandir / recolher submenus com animação
 *   3. Colapsar a sidebar no desktop (modo ícone)
 *   4. Fechar ao clicar no overlay ou pressionar Esc
 *   5. Marcar item ativo com base na URL atual
 */

(function ($) {
    'use strict';

    /* ── Referências DOM ───────────────────────────────────── */
    const $sidebar        = $('#sidebar');
    const $overlay        = $('#sidebarOverlay');
    const $toggleBtn      = $('#sidebarToggleBtn');
    const $closeBtn       = $('#sidebarCloseBtn');
    const $submenuToggles = $('.sidebar-link--submenu-toggle');

    /* ── Utilitários ────────────────────────────────────────── */
    const isMobile = () => $(window).width() < 768;

    /* ── 1. Abrir / fechar sidebar no mobile ────────────────── */
    function openSidebar() {
        $sidebar.addClass('is-open').attr('aria-hidden', 'false');
        $overlay.addClass('is-visible').removeAttr('aria-hidden');
        $toggleBtn.attr('aria-expanded', 'true');
        // Foca no primeiro item do menu para acessibilidade
        $sidebar.find('.sidebar-link').first().trigger('focus');
    }

    function closeSidebar() {
        $sidebar.removeClass('is-open');
        $overlay.removeClass('is-visible').attr('aria-hidden', 'true');
        $toggleBtn.attr('aria-expanded', 'false');
        if (isMobile()) {
            $sidebar.attr('aria-hidden', 'true');
        }
        $toggleBtn.trigger('focus');
    }

    /* ── 2. Colapsar / expandir no desktop ──────────────────── */
    function toggleCollapse() {
        const isCollapsed = $('body').toggleClass('sidebar-collapsed').hasClass('sidebar-collapsed');
        if (isCollapsed) {
            closeAllSubmenus();
        }
        localStorage.setItem('sidebarCollapsed', isCollapsed ? '1' : '0');
    }

    /* Restaura preferência ao carregar */
    function restoreCollapseState() {
        if (!isMobile() && localStorage.getItem('sidebarCollapsed') === '1') {
            $('body').addClass('sidebar-collapsed');
        }
    }

    /* ── 3. Submenus ─────────────────────────────────────────── */
    function openSubmenu($btn, $submenu) {
        // Fecha outros submenus antes
        $submenuToggles.not($btn).each(function () {
            const $otherSub = $('#' + $(this).attr('aria-controls'));
            if ($otherSub.length && !$otherSub.prop('hidden')) {
                collapseSubmenu($(this), $otherSub);
            }
        });

        $btn.attr('aria-expanded', 'true');
        $submenu.prop('hidden', false);

        // Animação via slideDown do jQuery
        $submenu.hide().slideDown(280);
    }

    function collapseSubmenu($btn, $submenu) {
        $btn.attr('aria-expanded', 'false');
        $submenu.slideUp(220, function () {
            $(this).prop('hidden', true);
        });
    }

    function closeAllSubmenus() {
        $submenuToggles.each(function () {
            const $sub = $('#' + $(this).attr('aria-controls'));
            if ($sub.length && !$sub.prop('hidden')) {
                collapseSubmenu($(this), $sub);
            }
        });
    }

    function handleSubmenuToggle($btn) {
        if ($('body').hasClass('sidebar-collapsed') && !isMobile()) {
            // No modo recolhido, expande a sidebar primeiro
            $('body').removeClass('sidebar-collapsed');
            localStorage.setItem('sidebarCollapsed', '0');
        }

        const $submenu = $('#' + $btn.attr('aria-controls'));
        if (!$submenu.length) return;

        if ($submenu.prop('hidden')) {
            openSubmenu($btn, $submenu);
        } else {
            collapseSubmenu($btn, $submenu);
        }
    }

    /* ── 4. Marcar item ativo pela URL ───────────────────────── */
    function markActiveLink() {
        const currentPath = window.location.pathname.toLowerCase().replace(/\/$/, '');

        $sidebar.find('.sidebar-link:not(.sidebar-link--submenu-toggle), .sidebar-sublink').each(function () {
            const $link = $(this);
            const href  = ($link.attr('href') || '').toLowerCase().replace(/\/$/, '');
            if (!href || href === '#') return;

            const matches = currentPath === href || currentPath.startsWith(href + '/');

            if (matches) {
                $link.addClass('active').attr('aria-current', 'page');

                // Se for sublink, abre o submenu pai automaticamente
                const $parentSubmenu = $link.closest('.sidebar-submenu');
                if ($parentSubmenu.length) {
                    const $parentBtn = $('[aria-controls="' + $parentSubmenu.attr('id') + '"]');
                    if ($parentBtn.length) {
                        openSubmenu($parentBtn, $parentSubmenu);
                    }
                }
            } else {
                $link.removeClass('active').removeAttr('aria-current');
            }
        });
    }

    /* ── 5. Evento: botão hambúrguer ─────────────────────────── */
    $toggleBtn.on('click', function () {
        if (isMobile()) {
            $sidebar.hasClass('is-open') ? closeSidebar() : openSidebar();
        } else {
            toggleCollapse();
        }
    });

    /* ── 6. Evento: botão fechar (mobile) ────────────────────── */
    $closeBtn.on('click', closeSidebar);

    /* ── 7. Evento: overlay ──────────────────────────────────── */
    $overlay.on('click', closeSidebar);

    /* ── 8. Eventos: submenus ────────────────────────────────── */
    $submenuToggles.on('click', function () {
        handleSubmenuToggle($(this));
    });

    /* ── 9. Fechar com Esc ───────────────────────────────────── */
    $(document).on('keydown', function (e) {
        if (e.key === 'Escape' && isMobile() && $sidebar.hasClass('is-open')) {
            closeSidebar();
        }
    });

    /* ── 10. Ajustar no resize ───────────────────────────────── */
    let resizeTimer;
    $(window).on('resize', function () {
        clearTimeout(resizeTimer);
        resizeTimer = setTimeout(function () {
            if (!isMobile()) {
                $sidebar.removeClass('is-open').removeAttr('aria-hidden');
                $overlay.removeClass('is-visible').attr('aria-hidden', 'true');
                $toggleBtn.attr('aria-expanded', 'false');
            }
        }, 150);
    });

    /* ── 11. Inicialização ───────────────────────────────────── */
    function init() {
        restoreCollapseState();
        markActiveLink();

        if (isMobile()) {
            $sidebar.attr('aria-hidden', 'true');
        }
    }

    $(document).ready(init);

}(jQuery));