﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.17929
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsServiceWcfServiceLibraryVector.WcfServiceLibrary {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WcfServiceLibrary.IRole")]
    public interface IRole {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRole/GetName", ReplyAction="http://tempuri.org/IRole/GetNameResponse")]
        string GetName(string name);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRoleChannel : WindowsServiceWcfServiceLibraryVector.WcfServiceLibrary.IRole, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RoleClient : System.ServiceModel.ClientBase<WindowsServiceWcfServiceLibraryVector.WcfServiceLibrary.IRole>, WindowsServiceWcfServiceLibraryVector.WcfServiceLibrary.IRole {
        
        public RoleClient() {
        }
        
        public RoleClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RoleClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoleClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RoleClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetName(string name) {
            return base.Channel.GetName(name);
        }
    }
}