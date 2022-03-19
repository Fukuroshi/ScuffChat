    GRANT SELECT ON TABLE public.dms TO chat;
    GRANT SELECT ON TABLE public.messages TO chat;
    GRANT SELECT ON TABLE public.users TO chat;
    GRANT insert ON TABLE public.dms TO chat;
    GRANT insert ON TABLE public.messages TO chat;
    GRANT insert ON TABLE public.users TO chat;
    GRANT update ON TABLE public.dms TO chat;
    GRANT update ON TABLE public.messages TO chat;
    GRANT update ON TABLE public.users TO chat;
    GRANT EXECUTE ON PROCEDURE public.messagesend(character varying, character varying) TO chat;
    GRANT EXECUTE ON PROCEDURE public.online(character varying) TO chat;
    GRANT EXECUTE ON PROCEDURE public.offline(character varying) TO chat;
    GRANT EXECUTE ON PROCEDURE public.register(character varying, character varying) TO chat;
    GRANT EXECUTE ON PROCEDURE public.senddm(character varying, character varying, character varying) TO chat;
    GRANT EXECUTE ON FUNCTION public.login(character varying, character varying) TO chat;
    GRANT EXECUTE ON FUNCTION public.messageList() TO chat;
    GRANT EXECUTE ON FUNCTION public.messageAmount() TO chat;
    GRANT EXECUTE ON FUNCTION public.dmList(character varying, character varying) TO chat;
    GRANT EXECUTE ON FUNCTION public.dmAmount(character varying, character varying) TO chat;
    GRANT EXECUTE ON FUNCTION public.isOnline(character varying) TO chat;
    GRANT EXECUTE ON FUNCTION public.onlineUsers() TO chat;
    GRANT EXECUTE ON FUNCTION public.offlineUsers() TO chat;
    GRANT EXECUTE ON FUNCTION public.onlineUserAmount() TO chat;
    grant execute on function gen_salt(text) to chat
    
