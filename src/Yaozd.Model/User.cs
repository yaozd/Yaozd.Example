//==============================================================================
//	CAUTION: This file is generated  at 2015-11-4 10:22:58
//==============================================================================
using System;

namespace Yaozd.Model {
	
    [Serializable]
    public partial class User {
        
        #region Id

        private Int32 m_id;
		
		/// <summary>Gets or sets Id</summary>
        public Int32 Id {
        	get { return m_id; }
        	set { m_id = value;}        
        }
		
	    #endregion
		
        #region Name

        private String m_name;
		
		/// <summary>Gets or sets Name</summary>
        public String Name {
        	get { return m_name; }
        	set { m_name = value;}        
        }
		
	    #endregion
		
        #region Password

        private String m_password;
		
		/// <summary>Gets or sets Password</summary>
        public String Password {
        	get { return m_password; }
        	set { m_password = value;}        
        }
		
	    #endregion
		
        #region UpdateTimeDefDateVal

        private DateTime? m_updateTimeDefDateVal;
		
		/// <summary>Gets or sets UpdateTimeDefDateVal</summary>
        public DateTime? UpdateTimeDefDateVal {
        	get { return m_updateTimeDefDateVal; }
        	set { m_updateTimeDefDateVal = value;}        
        }
		
	    #endregion
		
        #region CreateTimeDefTimeVal

        private DateTime? m_createTimeDefTimeVal;
		
		/// <summary>Gets or sets CreateTimeDefTimeVal</summary>
        public DateTime? CreateTimeDefTimeVal {
        	get { return m_createTimeDefTimeVal; }
        	set { m_createTimeDefTimeVal = value;}        
        }
		
	    #endregion
		
        #region IsDel

        private Int32? m_isDel;
		
		/// <summary>Gets or sets IsDel</summary>
        public Int32? IsDel {
        	get { return m_isDel; }
        	set { m_isDel = value;}        
        }
		
	    #endregion
		

	}
	
}
