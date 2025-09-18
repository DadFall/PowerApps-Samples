namespace PowerApps.Samples
{
    public static partial class Extensions
    {
        /// <summary>
        /// 转换 HttpResponseMessage to derived type
        /// </summary>
        /// <typeparam name="T">从 HttpResponseMessage 派生的类型</typeparam>
        /// <param name="response">HttpResponseMessage</param>
        /// <returns></returns>
        public static T As<T>(this HttpResponseMessage response) where T : HttpResponseMessage
        {
            T? typedResponse = (T)Activator.CreateInstance(typeof(T));

            //复制属性
            typedResponse.StatusCode = response.StatusCode;
            response.Headers.ToList().ForEach(h => {
                typedResponse.Headers.TryAddWithoutValidation(h.Key, h.Value);
            });
            typedResponse.Content = response.Content;
            return typedResponse;
        }
    }
}
