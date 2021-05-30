namespace TelegramBotApi.Profiles
{
    using AutoMapper;
    using TelegramBotApi.Models.Update;
    using TelegramBotApi.Types;

    internal class RequestProfile : Profile
    {
        public RequestProfile()
        {
            CreateMap<Update, Request>()
                .ForMember(x => x.ChatId, opt => opt.MapFrom(x => x.Message != null
                    ? x.Message.Chat.Id
                    : x.CallbackQuery!.Message.Chat.Id));

            CreateMap<Update, MessageRequest>()
                .ForMember(dst => dst.ChatId, opt => opt.MapFrom(x => x.Message!.Chat.Id))
                .ForMember(dst => dst.Text, opt => opt.MapFrom(x => x.Message!.Text))
                .ForMember(dst => dst.MessageId, opt => opt.MapFrom(x => x.Message!.Id));

            CreateMap<Update, QueryRequest>()
                .ForMember(dst => dst.ChatId, opt => opt.MapFrom(x => x.CallbackQuery!.Message.Chat.Id))
                .ForMember(dst => dst.QueryId, opt => opt.MapFrom(x => x.CallbackQuery!.Id))
                .ForMember(dst => dst.MessageId, opt => opt.MapFrom(x => x.CallbackQuery!.Message.Id))
                .ForMember(dst => dst.Query, opt => opt.MapFrom(x => new Query(x.CallbackQuery!.Data)));
        }
    }
}
